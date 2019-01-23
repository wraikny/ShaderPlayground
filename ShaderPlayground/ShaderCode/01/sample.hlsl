Texture2D g_texture : register( t0 );
SamplerState g_sampler : register( s0 );

struct PS_Input
{
    float4 SV_Position : SV_POSITION;
    float4 Position : POSITION;
    float2 UV : UV;
    float4 Color : COLOR;
};

vec2 windowSize;
float time;

// black box
float rand(vec2 co, vec2 t) {
    return fract(sin(dot(co.xy , t + vec2(12.9898, 78.233))) * (43758.5453 + time));
}

vec3 blockNoiseColor() {
    float a = rand(floor(inUV * 10), vec2(0, 0));
    float b = rand(floor(inUV * 10), vec2(3.2374, 5.1293));
    return vec3(a, b, (1.0 + sin(time))/2.0);
}

float4 main( const PS_Input Input ) : SV_Target
    return vec4(blockNoiseColor(), 1);
}