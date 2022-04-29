#version 330

out vec4 FragColor;

in vec2 texCoord;

uniform vec4 tintColor;
uniform sampler2D texture0;

void main()
{
    FragColor = texture(texture0,texCoord);
}