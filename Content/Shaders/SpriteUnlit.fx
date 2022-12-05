//(OBSOLETE) USE TERMINAL COMMAND TO COMPILE:
//mgfxc "Content/Shaders/SpriteUnlit.fx" "Content/Shaders/SpriteUnlitCompiled.fx" /Debug /Profile:DirectX_11

#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0

sampler2D TextureSampler : register(s0)
{
    Texture = (Texture);
};

float4x4 WorldMatrix;
float4x4 ViewMatrix;
float4x4 ProjectionMatrix;

struct VertexShaderInput
{
    float4 Position : SV_POSITION;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float2 TexCoord : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, WorldMatrix);
    float4 viewPosition = mul(worldPosition, ViewMatrix);
    output.Position = mul(viewPosition, ProjectionMatrix);

    output.TexCoord = input.TexCoord;

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input, bool front:  VFACE) : SV_Target
{
    float4 color = tex2D(TextureSampler, input.TexCoord);

    //uncomment to pixelate
    // float pixelRatio = 5;
    // float2 texCoord = input.TexCoord * pixelRatio;
    // texCoord = round(texCoord);
    // texCoord /= pixelRatio;
    // color = tex2D(TextureSampler, texCoord);
    
    return color;
}

technique SpriteUnlit
{
    pass Pass1
    {
        VertexShader = compile VS_SHADERMODEL VertexShaderFunction();
        PixelShader = compile PS_SHADERMODEL PixelShaderFunction();
    }
}