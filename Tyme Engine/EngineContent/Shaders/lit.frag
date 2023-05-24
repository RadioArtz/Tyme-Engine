#version 330
in vec3 Normal;
in vec3 FragPos;
in vec2 texCoord;

out vec4 FragColor;
uniform vec3 viewPos;
uniform sampler2D texture0;
uniform vec3 lightPos;
uniform vec3 DiffuseColor;
uniform vec3 SpecularColor;
uniform vec3 AmbientColor;
uniform vec4 LightColor;
uniform float radius;

void main()
{
	vec4 texture1Sample = texture(texture0,texCoord);
	if(texture1Sample.a <0.1)//transparency
		discard;	

	vec3 _LightCol = vec3(LightColor.rgb)*LightColor.w;						//using the W component of the Light color to scale brightness

	vec3 norm = normalize(Normal);
	vec3 lightDir = normalize((lightPos)-FragPos);

	float dist = distance(FragPos,lightPos);
	//float rad = sqrt(1.5
	//float falloff = clamp(((dist*(radius)) / (dist*dist)/1f),0f,1f);	//this is kinda wonky rn, maybe like... make it good or smth in the future
	//float falloff = clamp(1.5 - dist*dist/(radius*radius),0.0,1.0);
	//falloff *= falloff;
	float falloff = clamp(((dist*(radius)) / (dist*dist)/1f),0f,1f);
	falloff*=falloff;

	float diff = max(dot(norm,lightDir),0.0);
	vec3 diffResult = diff* _LightCol;
	

	vec3 viewDir = normalize((viewPos) - FragPos);
	vec3 reflectDir = reflect(-lightDir, norm);

	float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32)*1;
	spec = spec*falloff;
	vec3 specular = SpecularColor * spec * _LightCol;

	specular *= vec3(texture1Sample.r + texture1Sample.g + texture1Sample.b);

	texture1Sample.rgb *= DiffuseColor;

	vec3 result = ((diffResult*falloff) + (specular*falloff) + AmbientColor) * texture1Sample.rgb;

	float gamma = 2.2;
	result = pow(result,vec3(1.0/gamma));//apply gamma correction on final image
	result *= result;
	FragColor = vec4(result,1);
}