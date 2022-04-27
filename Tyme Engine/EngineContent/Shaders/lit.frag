#version 330
in vec3 Normal;
in vec3 FragPos;
in vec2 texCoord;

out vec4 FragColor;

uniform vec3 lightPos;
uniform vec3 DiffuseColor;
uniform vec3 SpecularColor;
uniform vec3 AmbientColor;
uniform vec3 LightColor;

void main()
{
	vec3 norm = normalize(Normal);
	vec3 lightDir = normalize(lightPos-FragPos);

	float diff = max(dot(norm,lightDir),0.0);
	vec3 diffResult = diff * LightColor;

	vec3 result = (AmbientColor + diffResult)*DiffuseColor + SpecularColor;
	FragColor = vec4(result,1);
}