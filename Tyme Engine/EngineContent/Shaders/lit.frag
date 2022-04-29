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
uniform vec3 LightColor;

void main()
{
	vec3 norm = normalize(Normal);
	vec3 lightDir = normalize((lightPos*-1)-FragPos); //multiplying lightpos with -1 cause for some reason the lightsource moves in the opposite direction of where it actually is?

	float diff = max(dot(norm,lightDir),0.0);
	vec3 diffResult = diff * LightColor;
	
	vec3 viewDir = normalize((viewPos*-1) - FragPos);
	vec3 reflectDir = reflect(-lightDir, norm);

	float spec = pow(max(dot(viewDir, reflectDir), 0.0), 2);
	vec3 specular = SpecularColor * spec * LightColor;

	//vec3 result = ((vec3(texture(texture0,texCoord)))*DiffuseColor) * diffResult + specular + AmbientColor;
	vec3 result = vec3(texture(texture0,texCoord))*(DiffuseColor*diffResult+AmbientColor)+specular;
	FragColor = vec4(result,1);
}