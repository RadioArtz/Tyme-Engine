#version 330 core

in vec3 aPos;
in vec2 aTexCoord;
out vec2 texCoord;

uniform mat4 transform;

uniform mat4 view;
uniform mat4 projection;

void main(void)
{
    texCoord = aTexCoord;
    gl_Position = vec4(aPos, 1.0f) * transform * view * projection;
}