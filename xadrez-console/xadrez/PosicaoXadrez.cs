using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez
    {

        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        // Convertendo posição de xadrez para uma posição matriz
        public Posicao paraPosicao()
        {
            // Para a coluna, operações com os códigos das letras (utilizando tabela ASCII)
            // docs.microsoft.com/pt-br/dotnet/csharp/programming-guide/strings/#related-topics

            // Só funciona com letras minúsculas
            return new Posicao(8 - linha, coluna - 'a');
        }

        public override string ToString()
        {
            return "" + coluna + linha;
        }
    }
}
