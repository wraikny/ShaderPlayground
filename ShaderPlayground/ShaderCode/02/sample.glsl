uniform sampler2D g_texture;

in vec4 inPosition;
in vec2 inUV;
in vec4 inColor;

uniform vec2 windowSize;
uniform float time;

out vec4 outOutput;

const int cubesNum = 8;
//const int sparse = 5;

int sparse() {
    return int(
        2
    );
}

struct Ray {
    vec3 pos;
    vec3 dir;
};

bool isExist(vec3 pos) {
    const float r = 0.3;
    const float radius = 1;
    bool inCube = (
        // length(pos) <= radius
        (abs(pos.x) < r) &&
        (abs(pos.y) < r) &&
        (abs(pos.z) < r)
    );

    vec3 v = floor(pos / r * cubesNum);


    return (
        inCube &&
        (mod((v.x + v.y + v.z), sparse()) == 0)
    );
}

void main_() {
    vec2 resolution = windowSize / windowSize.x;
    vec2 p = (inUV - 0.5) * resolution; //(inUV * 2.0 - resolution) / min(resolution.x, resolution.y);

    vec3 cameraPos = vec3(0, 0, 2);

    vec3 cameraDir = vec3(0.0,  0.0, -1.0);
    vec3 cameraUp  = vec3(0.0,  1.0,  0.0);
    vec3 cameraSide = cross(cameraDir, cameraUp);
    float targetDepth = 1.0;

    // ray
    vec3 dir = normalize(cameraSide * p.x + cameraUp * p.y + cameraDir * targetDepth);

    Ray ray;
    ray.pos = cameraPos;
    ray.dir = dir;

    // marching loop
    const float distance = 0.01;
    bool exist = false;

    float rLen = 0.0;
    vec3  rPos = ray.pos;

    for(int _i = 0; _i < 128; _i++){
        exist = isExist(rPos);
        rLen += distance;
        rPos = ray.pos + ray.dir * rLen;
    }
    
    vec3 color;
    // hit check
    if(exist){
        color = vec3(1, 0, 0);
    }else{
        color = vec3(0, 0, 1);
    }

    outOutput = vec4(color, 1);
}

void main() {
    // 座標正規化
    vec2 resolution = windowSize / windowSize.x;
    vec2 p = (inUV - 0.5) * resolution;

    // カメラ
    float t = time * 0.3;
    vec3 cameraPos = vec3(3 * cos(t), 1, 3 * sin(t));
    vec3 cameraDir = -normalize(cameraPos);
    vec3 cameraUp  = vec3(0.0,  1.0,  0.0);
    vec3 cameraSide = cross(cameraDir, cameraUp);
    float targetDepth = 2.1;
    vec3 dir = normalize(cameraSide * p.x + cameraUp * p.y + cameraDir * targetDepth);

    // レイ
    Ray ray;
    ray.pos = cameraPos;
    ray.dir = dir;

    const float distance = 0.01;

    // 更新用の変数
    bool exist = false;
    float rLen = 0.0;
    vec3  rPos = ray.pos;

    // marching loop
    for(int _i = 0; _i < 512; _i++){
        rLen += distance;
        rPos = ray.pos + ray.dir * rLen;

        exist = isExist(rPos);
        if(exist) {
            break;
        }
    }

    vec3 color;
    // hit check
    if(exist){
        float a = abs(dot(ray.dir, normalize(rPos)));
        float b = (1.0 - clamp(rLen - 2.5, 0.0, 1.0) );

        vec3 v = floor(rPos / 0.3 * cubesNum) / cubesNum / 2.0 + 0.5;
        

        color = v * b;
    }else{
        color = vec3(0, 0, 0);
    }

    outOutput = vec4(color, 1);
}