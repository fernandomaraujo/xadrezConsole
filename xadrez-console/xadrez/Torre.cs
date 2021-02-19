using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {

        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override string ToString()
        {
            return " T";
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

            // Marcando na matriz as posições onde a Torre pode se mover

            Posicao pos = new Posicao(0, 0);

            // Acima (uma linha a menos, mesma coluna)
            pos.definirValores(posicao.linha - 1, posicao.coluna);

            // Pode mover, enquanto estiver posições livres E peça adversária
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                // Enquanto for verdade, pode-se mover
                matriz[pos.linha, pos.coluna] = true;

                // Caso seja barrado por uma peça adversária
                // Se tiver peça (diferente de nulo) E cor da peça for diferente (peça adversária)
                if(tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
                {
                    // Forçar a parada do while
                    break;
                }

                // Caso não pare, continua verificando as posições acima
                // Atualiza a linha fazendo ela ir pra próxima posição acima
                pos.linha = pos.linha - 1;

            }

            // Abaixo (uma linha a mais, mesma coluna)
            pos.definirValores(posicao.linha + 1, posicao.coluna);

            // Pode mover, enquanto estiver posições livres E peça adversária
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                // Enquanto for verdade, pode-se mover
                matriz[pos.linha, pos.coluna] = true;

                // Caso seja barrado por uma peça adversária
                // Se tiver peça (diferente de nulo) E cor da peça for diferente (peça adversária)
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
                {
                    // Forçar a parada do while
                    break;
                }

                // Caso não pare, continua verificando as posições abaixo
                // Atualiza a linha fazendo ela ir pra próxima posição abaixo
                pos.linha = pos.linha + 1;

            }

            // Direita (mesma linha, uma coluna a mais)
            pos.definirValores(posicao.linha, posicao.coluna + 1);

            // Pode mover, enquanto estiver posições livres E peça adversária
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                // Enquanto for verdade, pode-se mover
                matriz[pos.linha, pos.coluna] = true;

                // Caso seja barrado por uma peça adversária
                // Se tiver peça (diferente de nulo) E cor da peça for diferente (peça adversária)
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
                {
                    // Forçar a parada do while
                    break;
                }

                // Caso não pare, continua verificando as posições a direita
                // Atualiza a coluna fazendo ela ir pra próxima posição a direita
                pos.coluna = pos.coluna + 1;

            }

            // Esquerda (mesma linha, uma coluna a menos)
            pos.definirValores(posicao.linha, posicao.coluna - 1);

            // Pode mover, enquanto estiver posições livres E peça adversária
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                // Enquanto for verdade, pode-se mover
                matriz[pos.linha, pos.coluna] = true;

                // Caso seja barrado por uma peça adversária
                // Se tiver peça (diferente de nulo) E cor da peça for diferente (peça adversária)
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
                {
                    // Forçar a parada do while
                    break;
                }

                // Caso não pare, continua verificando as posições a esquerda
                // Atualiza a coluna fazendo ela ir pra próxima posição a esquerda
                pos.coluna = pos.coluna - 1;

            }

            return matriz;
        }
    }
}

