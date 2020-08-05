uniform sampler2D texture;

void main()
{
  vec4 pixel = texture2D(texture, gl_TexCoord[0].xy);
  gl_FragColor = vec4(pixel) * gl_Color;
}

