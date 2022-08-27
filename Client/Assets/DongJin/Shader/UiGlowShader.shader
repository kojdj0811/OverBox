Shader "Hidden/UiGlowShader"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _GlowEnable ("Glow Enable", float) = 0.0
        _GlowIntensity ("Glow Intensity", Range(0.0, 1.0)) = 1.0
        _GlowThickness ("Glow Thickness", float) = 1.0
        _GlowColor ("Glow Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }



        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "Default"
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;

            fixed _GlowEnable;
            float _GlowIntensity;
            float _GlowThickness;
            fixed4 _GlowColor;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                half4 texColor = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd);
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif





                // color.a = tex2D(_MainTex, IN.texcoord + _MainTex_TexelSize.xy*_GlowThickness).a;


                float alpha = 0.0;
                for(int y = -_GlowThickness; y < _GlowThickness; y++) {
                    for(int x = -_GlowThickness; x < _GlowThickness; x++) {
                        if(length(IN.texcoord - float2(x,y) * _MainTex_TexelSize.xy) < _GlowThickness) {

                            float alphaCheck = tex2D(_MainTex, IN.texcoord - float2(x,y) * _MainTex_TexelSize.xy).a;
                            if(alphaCheck  > 0.0)
                                alpha += alphaCheck;

                                // color.a = 1.0;

                        }
                    }
                }
                color.a = _GlowEnable < 1.0 ? color.a : max(color.a, saturate(alpha/(_GlowThickness*_GlowThickness*2.0) *_GlowIntensity));
                color.rgb = _GlowEnable < 1.0 ? color.rgb : (color.a > 0.999 ? color.rgb : lerp(_GlowColor, color.rgb, pow(color.a, 20.0)));

                // float alpha = 0.0;
                // for(int y = -_GlowThickness*0.5; y < _GlowThickness*0.5; y += _MainTex_TexelSize.y) {
                //     for(int x = -_GlowThickness*0.5; x < _GlowThickness*0.5; x += _MainTex_TexelSize.x) {

                //         alpha += (tex2D(_MainTex, IN.texcoord + _MainTex_TexelSize.xy*float(x,y)) + _TextureSampleAdd);
                //     }
                // }

                // alpha /= _GlowThickness*_GlowThickness;
                // color.a = alpha;



                return color;
            }
        ENDCG
        }
    }
}
