using tabuleiro;

namespace xadrez
{

    class Peao : Peca
    {

        private PartidaDeXadrez partida;

        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return " P";
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Posicao pos)
        {
            return tab.peca(pos) == null;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branca)
            {
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    matriz[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 2, posicao.coluna);
                Posicao p2 = new Posicao(posicao.linha - 1, posicao.coluna);
                if (tab.posicaoValida(p2) && livre(p2) && tab.posicaoValida(pos) && livre(pos) && quantMovimentos == 0)
                {
                    matriz[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    matriz[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    matriz[pos.linha, pos.coluna] = true;
                }


                // Jogada Especial: En Passant (Peças brancas)
                if (posicao.linha == 3)
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);

                    // Se existe um peão inimigo na posição a esquerda e se ele está vulnerável
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        matriz[esquerda.linha - 1, esquerda.coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);

                    // Se existe um peão inimigo na posição a direita e se ele está vulnerável
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant)
                    {
                        matriz[direita.linha - 1, direita.coluna] = true;
                    }
                }
            }
            else
            {

                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    matriz[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha + 2, posicao.coluna);
                Posicao p2 = new Posicao(posicao.linha + 1, posicao.coluna);
                if (tab.posicaoValida(p2) && livre(p2) && tab.posicaoValida(pos) && livre(pos) && quantMovimentos == 0)
                {
                    matriz[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    matriz[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    matriz[pos.linha, pos.coluna] = true;
                }


                // Jogada Especial: En Passant (Peças pretas)
                if (posicao.linha == 4)
                {

                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);

                    // Se existe um peão inimigo na posição a esquerda e se ele está vulnerável
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        matriz[esquerda.linha + 1, esquerda.coluna] = true;
                    }

                    // Se existe um peão inimigo na posição a direita e se ele está vulnerável
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant)
                    {
                        matriz[direita.linha + 1, direita.coluna] = true;
                    }
                }
            }

            return matriz;
        }
    }
}