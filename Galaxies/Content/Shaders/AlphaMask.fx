#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
Texture2D MaskTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 position : SV_POSITION;
	float4 color : COLOR0;
	float2 texCoords : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	//float maskPixelX = input.texCoords.x;
	//float maskPixelY = input.texCoords.y;
	float4 bitMask = tex2D(MaskTexture, input.texCoords);

	float4 tex = tex2D(SpriteTexture, input.texCoords);
	
	return tex * (bitMask.a);

	//return tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};