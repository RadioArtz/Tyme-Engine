#version 330

out vec4 FragColor;

in vec2 texCoord;

uniform vec4 tintColor;
uniform sampler2D texture0;

void main()
{
	vec4 texture1Sample = texture(texture0,texCoord);
	if(texture1Sample.a <0.1)//transparency
		discard;
    FragColor = texture1Sample;
}