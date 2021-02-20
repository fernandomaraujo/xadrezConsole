namespace tabuleiro
{
    abstract class Peca
    {

        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int quantMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null;
            this.tab = tab;
            this.cor = cor;
            this.quantMovimentos = 0;
        }

        /*
            Matriz de booleanos

            true = Onde movimento for possível;
            false = Onde movimento não for possível;
         */
        public abstract bool[,] movimentosPossiveis();

        public void incrementarQuantMovimentos()
        {
            quantMovimentos++;
        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] matriz = movimentosPossiveis();
            int i, j;

            for(i=0; i<tab.linhas; i++)
            {

                for(j=0; j<tab.colunas; j++)
                {

                    // Se tem movimentos possíveis
                    if(matriz[i,j])
                    {
                        return true;
                    }
                }
            }

            // Não tem movimentos possíveis
            return false;
        }
    }
}
