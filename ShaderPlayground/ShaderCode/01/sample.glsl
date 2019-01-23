uniform sampler2D g_texture;

in vec4 inPosition;
in vec2 inUV;
in vec4 inColor;

uniform vec2 windowSize;
uniform float time;

out vec4 outOutput;

// black box
float rand(vec2 co, vec2 t) {
    return fract(sin(dot(co.xy , t + vec2(12.9898, 78.233))) * (43758.5453 + time));
}

vec3 blockNoiseColor() {
    // vec2 uv = inUV * windowSize;
    float a = rand(floor(inUV * 10), vec2(0, 0));
    float b = rand(floor(inUV * 10), vec2(3.2374, 5.1293));
    // vec2 b = floor(inUV * 2);
    return vec3(a, b, (1.0 + sin(time))/2.0);
}

void main() {
    outOutput = vec4(blockNoiseColor(), 1);
}