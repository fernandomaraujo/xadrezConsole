using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {

        private PartidaDeXadrez partida;

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor) {
            this.partida = partida;
        }

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

        // Testando se uma torre pode fazer roque
        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = tab.peca(pos);

            // Se a peça existir E for uma instância de Torre E mesma cor do Rei E quantidade de movimentos igual a 0
            return p != null && p is Torre && p.cor == cor && p.quantMovimentos == 0;
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

            // Jogada Especial: Roque
            if(quantMovimentos == 0 && !partida.xeque)
            {
                // Jogada Especial: Roque Pequeno
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);

                // Se está elegivel para roque
                if(testeTorreParaRoque(posT1))
                {
                    
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);

                    // Se posições estão livres
                    if (tab.peca(p1) == null && tab.peca(p2) == null)
                    {
                        matriz[posicao.linha, posicao.coluna + 2] = true;
                    }

                }

                // Jogada Especial: Roque Grande
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4);

                // Se está elegivel para roque
                if (testeTorreParaRoque(posT2))
                {

                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);

                    // Se posições estão livres
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null)
                    {
                        matriz[posicao.linha, posicao.coluna - 2] = true;
                    }

                }

            }

            // Marcado pra onde a peça pode se mover, retorna matriz
            return matriz;
        }
    }
}
