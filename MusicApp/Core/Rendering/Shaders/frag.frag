#version 330 core

out vec4 outputColor;

// This is where the color variable we declared and assigned in vertex shader 
// Gets pass to, this is enabled by using the in keyword 
// Keep in mind the vec type must match in order for this to work

uniform int isTextureLoaded = 0;
uniform int isLightLoaded = 0;
uniform vec3 viewPos;

//For Light
struct LightDependencies{
    float shiness;

    vec3 lightPos;
    vec3 lightColor;

    float ambientStrength;
    float specularStrength;

    float atten0;
    float atten1;
    float atten2;
};

//I will use just 5 light so we don't need to create an array
uniform LightDependencies light0;
uniform LightDependencies light1;
uniform LightDependencies light2;
uniform LightDependencies light3;
uniform LightDependencies light4;

in vec4 vertexColor;
in vec2 TexCoord;
in vec3 Normal;
in vec3 Position;
in vec3 FragPos;

uniform sampler2D Tex0;

vec4 GetLightInfos(LightDependencies light0){

    vec3 lightDir = normalize(light0.lightPos-FragPos);
 
    //Ambient

    vec3 ambient = light0.ambientStrength * light0.lightColor;

    //Diffuse
    vec3 norm = normalize(Normal);
    float diff = max(dot(norm,lightDir),0.0f);
    vec3 diffuse = diff * light0.lightColor;

    //Specular    
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir,norm);

    float spec = pow(max(dot(viewDir,reflectDir),0.0),light0.shiness);
   
    vec3 specular = light0.lightColor * light0.specularStrength * spec*5;   

    //Attenuation

    float distance = length(light0.lightPos - FragPos);

    float attenuation = light0.atten0/(light0.atten1*distance +light0.atten2 * (distance*distance));

    if(isTextureLoaded == 1){
    vec4 texColor = texture(Tex0,TexCoord);
    vec4 ambient2 = texColor * vec4(ambient,1.0f);
    vec4 diffuse2 = texColor * vec4(diffuse,1.0f);
    vec4 specular2 = texColor * vec4(specular,1.0f);
   

    ambient2 *= attenuation;
    diffuse2 *= attenuation;
    specular2 *= attenuation;

    return (specular2+ambient2+diffuse2);
    
    }else{
    ambient *= attenuation;
    diffuse *= attenuation;
    specular *= attenuation;
    return vec4((specular+diffuse+ambient),1.0f);
    }


}


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

    outputColor = GetLightInfos(light0) + GetLightInfos(light1) + GetLightInfos(light2) + GetLightInfos(light3) + GetLightInfos(light4);
        

    }
	
    //Specular
    
   
    
}