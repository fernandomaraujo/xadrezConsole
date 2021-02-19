namespace tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }

        // Matriz de peças
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;

            pecas = new Peca[linhas, colunas];
        }

        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

        // Sobrecarga
        public Peca peca(Posicao pos)
        {
            return pecas[pos.linha, pos.coluna];
        }

        // Se existe peça em determinada posição
        public bool existePeca(Posicao pos)
        {
            // Antes, conferindo se ela é válida
            validarPosicao(pos);

            // Se a peça na posição for diferente de nulo, existe peça na posição
            return peca(pos) != null;
        }

        public void colocarPeca(Peca p, Posicao pos)
        {

            if(existePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }

            pecas[pos.linha, pos.coluna] = p;

            p.posicao = pos;
        }

        public Peca retirarPeca(Posicao pos)
        {
            // Não tem peça nessa posição. Nenhuma a ser removida
            if(peca(pos) == null)
            {
                return null;
            }

            // Caso tenha, remove
            Peca aux = peca(pos);
            aux.posicao = null;

            // Marcando como nula a posição que ela estava no tabuleiro 
            pecas[pos.linha, pos.coluna] = null;

            // Retorna peça guardada
            return aux;
        }

        public bool posicaoValida(Posicao pos)
        {

            if(
                pos.linha < 0 || pos.linha >= linhas ||
                pos.coluna < 0 || pos.coluna >= colunas
            )
            {
                return false;
            }

            // Caso contrário
            return true;
        }

        public void validarPosicao(Posicao pos)
        {

            // Se posição não for válida
            if(!posicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}
