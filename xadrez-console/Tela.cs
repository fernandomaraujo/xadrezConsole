using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {

        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            imprimirTabuleiro(partida.tab);

            Console.WriteLine();

            imprimirPecasCapturadas(partida);

            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno);

            // Se a partida ainda não estiver terminada
            if (!partida.terminada)
            {

                Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);

                // Se o jogador estiver em xeque
                if (partida.xeque)
                {
                    Console.WriteLine("Você está em xeque!");
                }

            } else
            {
                Console.WriteLine("Xequemate!");
                Console.WriteLine("Vencedor: " + partida.jogadorAtual);
            }
        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas:");

            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));

            Console.WriteLine();

            Console.Write("Pretas:  ");

            // Alterando cor das peças pretas
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;

            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));

            // Depois de escrita, retorna para cor padrão do console
            Console.ForegroundColor = aux;

            Console.WriteLine();
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");

            foreach(Peca p in conjunto)
            {
                Console.Write(p + " ");
            }

            Console.Write("]");
        }

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
                    imprimirPeca(tab.peca(i, j));
                }

                Console.WriteLine();
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine("   | a  b  c  d  e  f  g  h");
            
        }

        // Sobrecarga
        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesDisponiveis)
        {
            // Tabuleiro
            int i, j;

            // Salvando a cor de fundo do console
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            // Linhas
            for (i = 0; i < tab.linhas; i++)
            {
                // Exibindo número da coluna
                Console.Write(" " + (8 - i) + " |");

                // Colunas
                for (j = 0; j < tab.colunas; j++)
                {

                    // Se posição permitir movimento
                    if(posicoesDisponiveis[i,j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    } else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }

                Console.WriteLine();
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine("   | a  b  c  d  e  f  g  h");

            // No final do método, retornar a cor original
            Console.BackgroundColor = fundoOriginal;

        }

        // Ler o que o usuário digitar
        public static PosicaoXadrez lerPosicaoXadrez()
        {

            string s = Console.ReadLine();

            // Letra
            char coluna = s[0];

            // Número
            int linha = int.Parse(s[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }

        // Alterando cores
        public static void imprimirPeca(Peca peca)
        {

            // Se não houver peça na posição linha e coluna
            if (peca == null)
            {
                Console.Write(" - ");
            }
            else
            {

                if (peca.cor == Cor.Branca)
                {
                    // Imprimindo peça normalmente
                    Console.Write(peca);
                }
                else
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

                Console.Write(" ");
            }

        }
    }
}
