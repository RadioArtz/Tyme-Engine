#version 330 core
in vec3 aPos;
in vec2 aTexCoord;
in vec3 aNormal;

out vec3 Normal;
out vec3 FragPos;
out vec2 texCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position =  vec4(aPos, 1.0) * model * view * projection;
    FragPos = vec3(vec4(aPos,1.0)*model);
    texCoord = aTexCoord;
    Normal = aNormal * mat3(transpose(inverse(model)));
}   