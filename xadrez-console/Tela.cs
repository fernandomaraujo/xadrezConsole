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
                // Colunas
                for (j = 0; j < tab.colunas; j++)
                {

                    // Se não houver peça na posição linha e coluna
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write(" - ");
                    }
                    else {

                        Console.Write(tab.peca(i, j) + " ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
