using System;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {

        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            // Tabuleiro
            int i, j;
            
            // Linhas
            for(i = 0; i<tab.linhas; i++)
            {
                // Exibindo número da coluna
                Console.Write(" " + (8 - i) + " |");

                // Colunas
                for (j = 0; j < tab.colunas; j++)
                {

                    // Se não houver peça na posição linha e coluna
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write(" - ");
                    }
                    else {

                        // Console.Write(tab.peca(i, j) + " ");
                        imprimirPeca(tab.peca(i, j));
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("----------------------------");
            // Console.WriteLine("___________________________");
            Console.WriteLine("   | a  b  c  d  e  f  g  h");
            
        }

        // Alterando cores
        public static void imprimirPeca(Peca peca)
        {

            if (peca.cor == Cor.Branca)
            {
                // Imprimindo peça normalmente
                Console.Write(peca);
            } else
            {
                // Salvando cor da letra no terminal
                ConsoleColor aux = Console.ForegroundColor;

                // E alterando
                Console.ForegroundColor = ConsoleColor.Magenta;

                // Imprimindo peça
                Console.Write(peca);

                // Voltando pra cor original
                Console.ForegroundColor = aux;
            }

        }
    }
}
