using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {

        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override string ToString()
        {
            return " R";
        }

        // Testando se pode mover pra tal posição
        private bool podeMover(Posicao pos)
        {

            // Pegando a peça que tá na posição
            Peca p = tab.peca(pos);

            // Pode mover com as seguintes condições:
            // Se nula, posição tá livre OU se a peça for de cor diferente (peca adversária)
            return p == null || p.cor != this.cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];

            // Marcando na matriz as posições onde o Rei pode se mover

            Posicao pos = new Posicao(0, 0);

            // Acima (uma linha a menos, mesma coluna)
            pos.definirValores(posicao.linha - 1, posicao.coluna);

            // Se posição for válida e pode-se mover ela
            if(tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
            }

            // Nordeste (uma linha a menos, uma coluna a mais)
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
            }

            // Direita (mesma linha, uma coluna a mais)
            pos.definirValores(posicao.linha, posicao.coluna + 1);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
            }

            // Sudeste (uma linha a mais, uma coluna a mais)
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
            }

            // Abaixo (uma linha a mais, mesma coluna)
            pos.definirValores(posicao.linha + 1, posicao.coluna);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
            }

            // Sudoeste (uma linha a mais, uma coluna a menos)
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
            }

            // Esquerda (mesma linha, uma coluna a menos)
            pos.definirValores(posicao.linha, posicao.coluna - 1);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
            }

            // Noroeste (uma linha a menos, uma coluna a menos)
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
            }

            // Marcado pra onde a peça pode se mover, retorna matriz
            return matriz;
        }
    }
}
