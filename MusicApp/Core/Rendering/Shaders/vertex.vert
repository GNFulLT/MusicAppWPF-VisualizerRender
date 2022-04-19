#version 330 core

// the position variable has attribute position 0
layout(location = 0) in vec3 aPosition; 
layout(location=1)in vec2 aTexCoord;
layout(location=2)in vec3 aNormal;
// This variable uses the keyword out in order to pass on the value to the 
// next shader down in the chain, in this case the frag shader


uniform vec4 aColor;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 Normal;
out vec4 vertexColor;
out vec2 TexCoord;
out vec3 Position;
out vec3 FragPos;
void main(void)
{
	// see how we directly give a vec3 to vec4's constructor
    gl_Position = vec4(aPosition, 1.0) * model * view * projection;

	// Here we assign the variable a dark red color to the out variable
	vertexColor = aColor;
	TexCoord = aTexCoord;

	Normal = aNormal;
	Position = aPosition;
	FragPos = vec3(vec4(aPosition,1.0)*model);
}