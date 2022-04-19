#version 330 core

out vec4 outputColor;

// This is where the color variable we declared and assigned in vertex shader 
// Gets pass to, this is enabled by using the in keyword 
// Keep in mind the vec type must match in order for this to work

uniform int isTextureLoaded = 0;

in vec4 vertexColor;
in vec2 TexCoord;

uniform sampler2D Tex0;

void main()
{
    if(isTextureLoaded == 1){
    vec4 texColor = texture(Tex0,TexCoord);
    
    outputColor = texColor * vertexColor;
    }
    else{
        outputColor = vertexColor; 
    }
    
}