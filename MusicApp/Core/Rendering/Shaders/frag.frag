#version 330 core

out vec4 outputColor;

// This is where the color variable we declared and assigned in vertex shader 
// Gets pass to, this is enabled by using the in keyword 
// Keep in mind the vec type must match in order for this to work

uniform int isTextureLoaded = 0;
uniform int isLightLoaded = 0;
uniform vec3 lightColor;
uniform vec3 lightPos;
uniform vec3 viewPos;

//For Light
struct LightDependencies{
    float shiness;

    float ambientStrength;
    float specularStrength;

    float atten0;
    float atten1;
    float atten2;
};

uniform LightDependencies light;

in vec4 vertexColor;
in vec2 TexCoord;
in vec3 Normal;
in vec3 Position;
in vec3 FragPos;

uniform sampler2D Tex0;

void main()
{

    if(isLightLoaded == 0){
        if(isTextureLoaded == 1){
    vec4 texColor = texture(Tex0,TexCoord);
    
    outputColor = texColor * vertexColor;
        }
    else{
        outputColor = vertexColor; 
    }

    }
    else{


    vec3 lightDir = normalize(lightPos-FragPos);

    //Ambient
    vec3 ambient = light.ambientStrength * lightColor;
    //Diffuse

    vec3 norm = normalize(Normal);
    float diff = max(dot(norm,lightDir),0.0f);
    vec3 diffuse = diff * lightColor;
    //Specular    
    vec3 viewDir = normalize(viewPos - FragPos);
    float a = 1;
    if (dot(norm, viewDir) < 0.0){
    norm = -norm;
    }

    vec3 reflectDir = reflect(-lightDir,norm);
    if(max(dot(viewDir,reflectDir),0.0) == 0){
    }
   
    float spec = pow(max(dot(viewDir,reflectDir),0.0),light.shiness);
   
    vec3 specular = lightColor * light.specularStrength * spec*5;   

    float distance = length(lightPos - FragPos);

    float attenuation = light.atten0/(light.atten1*distance +light.atten2 * (distance*distance)); 

     if(isTextureLoaded == 1){
     
    vec4 texColor = texture(Tex0,TexCoord);
    vec4 ambient2 = texColor * vec4(ambient*a,1.0f);
    vec4 diffuse2 = texColor * vec4(diffuse*a,1.0f);
    vec4 specular2 = texColor * vec4(specular*a,1.0f);
   

    ambient2 *= attenuation;
    diffuse2 *= attenuation;
    specular2 *= attenuation;

    outputColor = (specular2+ambient2+diffuse2);
    }
    else{
        outputColor = vertexColor; 
    }

    }
	
    //Specular
    
   
    
}