Shader "Custom/World Position Shader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)

        _XScrollSpeed("X Scroll Speed", Float) = 1
        _YScrollSpeed("Y Scroll Speed", Float) = 1

        _Scale ("Texture Scale", Float) = 1.0
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
 
        CGPROGRAM
        #pragma surface surf Standard
 
        sampler2D _MainTex;
        fixed4 _Color;
        float _XScrollSpeed;
        float _YScrollSpeed;
        float _Scale;
 
        struct Input {
            float3 worldNormal;
            float3 worldPos;
        };
 
        void surf (Input IN, inout SurfaceOutputStandard o) {
 
           

            if(abs(IN.worldNormal.y) > 0.5)
            {
                 fixed2 scrollUV = IN.worldPos.xz;
                fixed xScrollValue = _XScrollSpeed * _Time.x;
                fixed yScrollValue = _YScrollSpeed * _Time.x;
                scrollUV += fixed2(xScrollValue, yScrollValue);
                o.Albedo = tex2D(_MainTex, scrollUV* _Scale)* _Color;
            }
            else if(abs(IN.worldNormal.x) > 0.5)
            {
                fixed2 scrollUV = IN.worldPos.yz;
                fixed xScrollValue = _XScrollSpeed * _Time.x;
                fixed yScrollValue = _YScrollSpeed * _Time.x;
                scrollUV += fixed2(xScrollValue, yScrollValue);
                o.Albedo = tex2D(_MainTex, scrollUV* _Scale)* _Color;
            }
            else
            {
                fixed2 scrollUV = IN.worldPos.xy;
                fixed xScrollValue = _XScrollSpeed * _Time.x;
                fixed yScrollValue = _YScrollSpeed * _Time.x;
                scrollUV += fixed2(xScrollValue, yScrollValue);
                o.Albedo = tex2D(_MainTex, scrollUV* _Scale)* _Color;
            }
 
            o.Emission = o.Albedo;
        }
 
        ENDCG
    }
    FallBack "Diffuse"
}