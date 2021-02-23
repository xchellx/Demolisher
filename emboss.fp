uniform sampler2D diffuseMap;
uniform sampler2D bumpMap;
uniform float embossScale;

varying vec3 vertexPosition;
varying vec3 vertexNormal;
varying vec3 vertexBinormal;
varying vec3 vertexTangent;

void main()
{
	// Light
	vec3 lightVector = normalize(gl_LightSource[0].position.xyz - vertexPosition);
	vec4 lightColor = vec4(max(dot(vertexNormal, lightVector), 0.0) * gl_FrontMaterial.diffuse.rgb + gl_FrontMaterial.ambient.rgb, gl_FrontMaterial.diffuse.a);
	
	// Emboss
	vec2 embossCoord = gl_TexCoord[1].st;
	
	if (dot(lightVector, vertexNormal) >= 0.0)
	{
		embossCoord += normalize(vec2(dot(lightVector, vertexTangent), dot(lightVector, vertexBinormal))) / 128.0 * embossScale;
	}
	
	// Sample
	vec4 diffuseTexel = texture2D(diffuseMap, gl_TexCoord[0].st);
	vec4 bumpTexel = texture2D(bumpMap, gl_TexCoord[1].st);
	vec4 embossTexel = texture2D(bumpMap, embossCoord);
	bumpTexel.rgb *= 0.5;
	embossTexel = vec4((1.0 - embossTexel.r) * 0.5, (1.0 - embossTexel.g) * 0.5, (1.0 - embossTexel.b) * 0.5, embossTexel.a);
	
	// Combine
	gl_FragColor = (bumpTexel + embossTexel) * diffuseTexel * lightColor;
}