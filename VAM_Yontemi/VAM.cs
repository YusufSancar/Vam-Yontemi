using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAM_Yontemi
{
    public class VAM
    {
        private int[,] arrMatris;
        private int[] arrTalep, arrArz;
        private int satir, sutun;

        public VAM()
        {

        }
        public VAM(int[,] matrisDeger, int[] arrArz, int[] arrTalep)
        {
            this.arrMatris = matrisDeger;
            this.arrArz = arrArz;
            this.arrTalep = arrTalep;

            this.satir = this.arrMatris.GetLength(0);
            this.sutun = this.arrMatris.GetLength(1);
           /* for (int i = 0; i < arrArz.Length; i++)
            {
                Console.WriteLine("ARZ : "+ arrArz[i]);
            }
            for (int i = 0; i < arrTalep.Length; i++)
            {
                Console.WriteLine("TALEP : " + arrTalep[i]);
            }*/
        }

        internal void Hesapla(out ArrayList aciklama, out ArrayList arzlar, out ArrayList talepler, out ArrayList cezalar, out ArrayList atamaIndexler, out ArrayList atamaDegerler)
        {
            cezalar = new ArrayList();
            talepler = new ArrayList();
            arzlar = new ArrayList();
            aciklama = new ArrayList();
            atamaIndexler = new ArrayList();
            atamaDegerler = new ArrayList();
            // Cezalar Matrsinin yedeği alındı, üzerinde değişiklik yapabilmek için
            int[,] arrMatrisYedek = arrMatris.Clone() as int[,]; // new int[satir, sutun]; ;
            //arrMatris.CopyTo(arrMatrisYedek, 0);
            MatrisYazdir(arrMatrisYedek);

            int[] arrCezalar = new int[satir + sutun];
            SatirCezalariHesapla(arrMatrisYedek).CopyTo(arrCezalar, 0);
            SutunCezalariHesapla(arrMatrisYedek).CopyTo(arrCezalar, satir);
            Console.WriteLine("Cezalar: ");
            MatrisYazdir(arrCezalar);

            cezalar.Add(arrCezalar.Clone());


            int[,] matrisDegerler = new int[satir, sutun];

            // 
            int[] arrArzYedek = new int[arrArz.Length];
            arrArz.CopyTo(arrArzYedek, 0);
            Console.WriteLine("arrArzYedek.L : " + arrArzYedek.Length);

            arzlar.Add(arrArzYedek.Clone());

            int[] arrTalepYedek = new int[arrTalep.Length];
            arrTalep.CopyTo(arrTalepYedek, 0);

            talepler.Add(arrTalepYedek.Clone());

            Console.WriteLine("arrTalepYedek.L : " + arrTalepYedek.Length);

            int sumArz = arrArz.Sum();
            int sumTalep = arrTalep.Sum();
            /* for (int i = 0; i < arrCezalar.Length; i++)
             {
                 Console.WriteLine("Cezalar: " + arrCezalar[i]);
             }*/

            // Cezanın yeri
            int cezaIndex;
            int yatayIndex, dikeyIndex;
            int index = 10;
            int toplamMaliyet = 0;
            string sAciklama = "";
            string sToplamMaliyet = "";
            while ((sumTalep > 0 && sumArz > 0) && !Cezalar0Mi(arrCezalar))
            //while(index > 0)
            {
                sAciklama = "";
                cezaIndex = EnYuksekCeza(arrCezalar);
                //Console.WriteLine("En yüksek cezaIndex: " + cezaIndex);
                CezaIndexHesapla(arrMatrisYedek, cezaIndex, out yatayIndex, out dikeyIndex);

                

                //Console.WriteLine("Yatay Index: " + yatayIndex);
                //Console.WriteLine("Dikey Index: " + dikeyIndex);
                if(cezaIndex >= satir)
                {
                    sAciklama += "En yüksek ceza ," + arrCezalar[cezaIndex] + ", " + (dikeyIndex + 1) + ". sütununda oldu.\n";
                    Console.WriteLine("En yüksek ceza ," + arrCezalar[cezaIndex] + ", " + (dikeyIndex + 1) + ". sütununda oldu.");
                }
                else
                {
                    sAciklama += "En yüksek ceza ," + arrCezalar[cezaIndex] + ", " + (yatayIndex + 1) + ". satırında oldu.\n";
                    Console.WriteLine("En yüksek ceza ," + arrCezalar[cezaIndex] + ", " + (yatayIndex + 1) + ". satırında oldu.");
                }

                sAciklama += "En düşük değerli hücre (" + (yatayIndex + 1) + "," + (dikeyIndex + 1) + ") = " + arrMatris[yatayIndex, dikeyIndex] + ".\n";
                Console.WriteLine("En düşük değerli hücre (" + (yatayIndex + 1) + "," + (dikeyIndex + 1) + ") = " + arrMatris[yatayIndex, dikeyIndex] + ".");

                Console.WriteLine("Arz : " + arrArzYedek[yatayIndex]);
                Console.WriteLine("Talep: " + arrTalepYedek[dikeyIndex]);

                if (arrArzYedek[yatayIndex] - arrTalepYedek[dikeyIndex] >= 0)
                {
                    //Console.WriteLine("Arz - Talep : " + (arrArzYedek[yatayIndex] - arrTalepYedek[dikeyIndex]));
                    sAciklama += "Bu hücreye maksimum atama min(" + arrArzYedek[yatayIndex] + "," + arrTalepYedek[dikeyIndex] + ") = " + arrTalepYedek[dikeyIndex] + ".\n";
                    Console.WriteLine("Bu hücreye maksimum atama min(" + arrArzYedek[yatayIndex] + ", " + arrTalepYedek[dikeyIndex] + ") = " + arrTalepYedek[dikeyIndex] + "." );

                    sAciklama += (dikeyIndex + 1) + ". sütunun ihtiyacını karşıla ve arzı " + arrArzYedek[yatayIndex] + " 'dan(den) " + (arrArzYedek[yatayIndex] - arrTalepYedek[dikeyIndex]) + " 'e(a) " + "değiştir. (" + arrArzYedek[yatayIndex] + " - " + arrTalepYedek[dikeyIndex] + " = " +( arrArzYedek[yatayIndex] - arrTalepYedek[dikeyIndex]) + ").\n";
                    Console.WriteLine((dikeyIndex + 1)+ ". sütunun ihtiyacını karşıla ve arzı " + arrArzYedek[yatayIndex] + " 'dan(den) " + (arrArzYedek[yatayIndex] - arrTalepYedek[dikeyIndex]) + " 'e(a) " + "değiştir. (" + arrArzYedek[yatayIndex] + " - " + arrTalepYedek[dikeyIndex] + " = " + (arrArzYedek[yatayIndex] - arrTalepYedek[dikeyIndex]) + ").");

                    atamaIndexler.Add(new int[] { yatayIndex, dikeyIndex });
                    atamaDegerler.Add(arrTalepYedek[dikeyIndex]);

                    arrArzYedek[yatayIndex] = arrArzYedek[yatayIndex] - arrTalepYedek[dikeyIndex];

                    toplamMaliyet += arrMatris[yatayIndex, dikeyIndex] * arrTalepYedek[dikeyIndex];
                    sToplamMaliyet += " +" + arrMatris[yatayIndex, dikeyIndex] + " * " + arrTalepYedek[dikeyIndex];
                    //Console.WriteLine("Dikeyden alması gereken: " + arrTalepYedek[dikeyIndex]);
                    //Console.WriteLine("Maliyet Hesabı: " + arrMatris[yatayIndex, dikeyIndex] + " * " + arrTalepYedek[dikeyIndex]);

                    arrTalepYedek[dikeyIndex] = 0;
                    sumArz -= arrTalepYedek[dikeyIndex];
                    sumTalep -= arrTalepYedek[dikeyIndex];



                }
                else if (arrTalepYedek[dikeyIndex] - arrArzYedek[yatayIndex] >= 0)
                {
                    //Console.WriteLine("Talep - Arz : " + (-arrArzYedek[yatayIndex] + arrTalepYedek[dikeyIndex]));

                    sAciklama += "Bu hücreye maksimum atama min(" + arrArzYedek[yatayIndex] + "," + arrTalepYedek[dikeyIndex] + ") = " + arrArzYedek[yatayIndex] + ".\n";
                    Console.WriteLine("Bu hücreye maksimum atama min(" + arrArzYedek[yatayIndex] + ", " + arrTalepYedek[dikeyIndex] + ") = " + arrArzYedek[yatayIndex] + ".");

                    toplamMaliyet += arrMatris[yatayIndex, dikeyIndex] * arrArzYedek[yatayIndex];
                    sToplamMaliyet += " +" + arrMatris[yatayIndex, dikeyIndex] + " * " + arrArzYedek[yatayIndex];

                    //Console.WriteLine("Yataydan alması gereken: " + arrArzYedek[yatayIndex]);
                    //Console.WriteLine("Maliyet Hesabı: " + arrMatris[yatayIndex, dikeyIndex] + " * " + arrArzYedek[yatayIndex]);



                    sAciklama += (yatayIndex + 1) + ". satırından ihtiyacını karşıla ve talebi " + arrTalepYedek[dikeyIndex] + " 'dan(den) " + (arrTalepYedek[dikeyIndex] - arrArzYedek[yatayIndex]) + " 'e(a) " + "değiştir. (" + arrTalepYedek[dikeyIndex] + " - " + arrArzYedek[yatayIndex] + " = " + (arrTalepYedek[dikeyIndex] - arrArzYedek[yatayIndex]) + ").\n";
                    Console.WriteLine((yatayIndex + 1) + ". sütunun ihtiyacını karşıla ve talebi " + arrTalepYedek[dikeyIndex] + " 'dan(den) " + (arrTalepYedek[dikeyIndex] - arrArzYedek[yatayIndex]) + " 'e(a) " + "değiştir. (" + arrTalepYedek[dikeyIndex] + " - " + arrArzYedek[yatayIndex] + " = " + (arrTalepYedek[dikeyIndex] - arrArzYedek[yatayIndex]) + ").");

                    atamaIndexler.Add(new int[] { yatayIndex, dikeyIndex });
                    atamaDegerler.Add(arrArzYedek[yatayIndex]);

                    arrTalepYedek[dikeyIndex] = arrTalepYedek[dikeyIndex] - arrArzYedek[yatayIndex];
                    arrArzYedek[yatayIndex] = 0;
                    sumArz -= arrArzYedek[yatayIndex];
                    sumTalep -= arrArzYedek[yatayIndex];

                }

                if (arrArzYedek[yatayIndex] == 0)
                {
                    //Console.WriteLine(yatayIndex + " için arz bitti değerler yok ediliyor");
                    sAciklama += (yatayIndex + 1) + ". satır için arz bitti değerler yok edildi.\n";
                    Console.WriteLine((yatayIndex + 1) + ". satır için arz bitti değerler yok edildi.");


                    for (int i = 0; i < sutun; i++)
                        arrMatrisYedek[yatayIndex, i] = int.MaxValue;
                }
                if (arrTalepYedek[dikeyIndex] == 0)
                {
                    //Console.WriteLine(dikeyIndex + " için talep bitti değerler yok ediliyor");
                    sAciklama += (dikeyIndex + 1) + ". sütun için talep bitti değerler yok edildi.\n";
                    Console.WriteLine((dikeyIndex + 1) + ". sütun için talep bitti değerler yok edildi.");


                    for (int i = 0; i < satir; i++)
                        arrMatrisYedek[i, dikeyIndex] = int.MaxValue;
                }


                SatirCezalariHesapla(arrMatrisYedek).CopyTo(arrCezalar, 0);
                SutunCezalariHesapla(arrMatrisYedek).CopyTo(arrCezalar, satir);
                

                Console.WriteLine("Matris:");
                MatrisYazdir(arrMatrisYedek);
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Arzlar: ");
                MatrisYazdir(arrArzYedek);
                arzlar.Add(arrArzYedek.Clone());

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Talepler: ");
                MatrisYazdir(arrTalepYedek);
                talepler.Add(arrTalepYedek.Clone());

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Cezalar: ");
                MatrisYazdir(arrCezalar);
                cezalar.Add(arrCezalar.Clone());

                Console.WriteLine("Anlık Toplam Maliyet: " + toplamMaliyet);
                index--;
                aciklama.Add(sAciklama);

            }
            aciklama.Add("Bu yönteme göre en düşük taşıma maliyeti = " + sToplamMaliyet +" = " + toplamMaliyet + " olarak bulunur.\n");
            Console.WriteLine("Toplam Maliyet : " + toplamMaliyet);
        }

        public void Hesapla()
        {
            // Cezalar Matrsinin yedeği alındı, üzerinde değişiklik yapabilmek için
            int[,] arrMatrisYedek = arrMatris.Clone() as int[,]; // new int[satir, sutun]; ;
            //arrMatris.CopyTo(arrMatrisYedek, 0);
            MatrisYazdir(arrMatrisYedek);

            int[] arrCezalar = new int[satir + sutun];
            SatirCezalariHesapla(arrMatrisYedek).CopyTo(arrCezalar, 0);
            SutunCezalariHesapla(arrMatrisYedek).CopyTo(arrCezalar, satir);
            Console.WriteLine("Cezalar: ");
            MatrisYazdir(arrCezalar);
            
            int[,] matrisDegerler = new int[satir, sutun];

            // 
            int[] arrArzYedek = new int[arrArz.Length];
            arrArz.CopyTo(arrArzYedek, 0);
            Console.WriteLine("arrArzYedek.L : "+arrArzYedek.Length);

            int[] arrTalepYedek = new int[arrTalep.Length];
            arrTalep.CopyTo(arrTalepYedek, 0);
            Console.WriteLine("arrTalepYedek.L : " + arrTalepYedek.Length);

            int sumArz = arrArz.Sum();
            int sumTalep = arrTalep.Sum();
           /* for (int i = 0; i < arrCezalar.Length; i++)
            {
                Console.WriteLine("Cezalar: " + arrCezalar[i]);
            }*/
            
            // Cezanın yeri
            int cezaIndex;
            int yatayIndex, dikeyIndex;
            int index = 10;
            int toplamMaliyet = 0;
            while ((sumTalep > 0 && sumArz > 0) && !Cezalar0Mi(arrCezalar))
            //while(index > 0)
            {
                cezaIndex = EnYuksekCeza(arrCezalar);
                Console.WriteLine("En yüksek cezaIndex: " + cezaIndex);
                CezaIndexHesapla(arrMatrisYedek, cezaIndex, out yatayIndex, out dikeyIndex);

                Console.WriteLine("Yatay Index: "+yatayIndex);
                Console.WriteLine("Dikey Index: "+dikeyIndex);

                Console.WriteLine("Arz : " + arrArzYedek[yatayIndex]);
                Console.WriteLine("Talep: " + arrTalepYedek[dikeyIndex]);

                if (arrArzYedek[yatayIndex] - arrTalepYedek[dikeyIndex] >= 0)
                {
                    Console.WriteLine("Arz - Talep : " + (arrArzYedek[yatayIndex] - arrTalepYedek[dikeyIndex]));
                    arrArzYedek[yatayIndex] = arrArzYedek[yatayIndex] - arrTalepYedek[dikeyIndex];

                    toplamMaliyet += arrMatris[yatayIndex, dikeyIndex] * arrTalepYedek[dikeyIndex];
                    Console.WriteLine("Dikeyden alması gereken: " + arrTalepYedek[dikeyIndex]);
                    Console.WriteLine("Maliyet Hesabı: " + arrMatris[yatayIndex, dikeyIndex] + " * " + arrTalepYedek[dikeyIndex]);

                    arrTalepYedek[dikeyIndex] = 0;
                    sumArz -= arrTalepYedek[dikeyIndex];
                    sumTalep -= arrTalepYedek[dikeyIndex];

                    

                }
                else if (arrTalepYedek[dikeyIndex] - arrArzYedek[yatayIndex] >= 0)
                {
                    Console.WriteLine("Talep - Arz : " + ( -arrArzYedek[yatayIndex] + arrTalepYedek[dikeyIndex]));

                    toplamMaliyet += arrMatris[yatayIndex, dikeyIndex] * arrArzYedek[yatayIndex];
                    Console.WriteLine("Yataydan alması gereken: " + arrArzYedek[yatayIndex]);
                    Console.WriteLine("Maliyet Hesabı: " + arrMatris[yatayIndex, dikeyIndex] + " * " + arrArzYedek[yatayIndex]);

                    arrTalepYedek[dikeyIndex] = arrTalepYedek[dikeyIndex] - arrArzYedek[yatayIndex];
                    arrArzYedek[yatayIndex] = 0;
                    sumArz -= arrArzYedek[yatayIndex];
                    sumTalep -= arrArzYedek[yatayIndex];

                }

                if(arrArzYedek[yatayIndex] == 0)
                {
                    Console.WriteLine(yatayIndex + " için arz bitti değerler yok ediliyor");
                    for (int i = 0; i < sutun; i++)
                        arrMatrisYedek[yatayIndex, i] = int.MaxValue;
                }
                if (arrTalepYedek[dikeyIndex] == 0)
                {
                    Console.WriteLine(dikeyIndex + " için talep bitti değerler yok ediliyor");
                    for (int i = 0; i < satir; i++)
                        arrMatrisYedek[i, dikeyIndex] = int.MaxValue;
                }


                SatirCezalariHesapla(arrMatrisYedek).CopyTo(arrCezalar, 0);
                SutunCezalariHesapla(arrMatrisYedek).CopyTo(arrCezalar, satir);
                Console.WriteLine("Matris:");
                MatrisYazdir(arrMatrisYedek);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Arzlar: ");
                MatrisYazdir(arrArzYedek);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Talepler: ");
                MatrisYazdir(arrTalepYedek);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Cezalar: ");
                MatrisYazdir(arrCezalar);

                Console.WriteLine("Anlık Toplam Maliyet: "+toplamMaliyet);
                index--;
                
            }
            Console.WriteLine("Toplam Maliyet : "+toplamMaliyet);
        }

        public string sHesapla()
        {
            // Cezalar Matrsinin yedeği alındı, üzerinde değişiklik yapabilmek için
            int[,] arrMatrisYedek = arrMatris.Clone() as int[,]; // new int[satir, sutun]; ;
            //arrMatris.CopyTo(arrMatrisYedek, 0);
            MatrisYazdir(arrMatrisYedek);

            int[] arrCezalar = new int[satir + sutun];
            SatirCezalariHesapla(arrMatrisYedek).CopyTo(arrCezalar, 0);
            SutunCezalariHesapla(arrMatrisYedek).CopyTo(arrCezalar, satir);
            Console.WriteLine("Cezalar: ");
            MatrisYazdir(arrCezalar);

            int[,] matrisDegerler = new int[satir, sutun];

            // 
            int[] arrArzYedek = new int[arrArz.Length];
            arrArz.CopyTo(arrArzYedek, 0);
            Console.WriteLine("arrArzYedek.L : " + arrArzYedek.Length);

            int[] arrTalepYedek = new int[arrTalep.Length];
            arrTalep.CopyTo(arrTalepYedek, 0);
            Console.WriteLine("arrTalepYedek.L : " + arrTalepYedek.Length);

            int sumArz = arrArz.Sum();
            int sumTalep = arrTalep.Sum();
            /* for (int i = 0; i < arrCezalar.Length; i++)
             {
                 Console.WriteLine("Cezalar: " + arrCezalar[i]);
             }*/

            // Cezanın yeri
            int cezaIndex;
            int yatayIndex, dikeyIndex;
            int index = 10;
            int toplamMaliyet = 0;
            string sToplamMaliyet = "";
            while ((sumTalep > 0 && sumArz > 0) && !Cezalar0Mi(arrCezalar))
            //while(index > 0)
            {
                cezaIndex = EnYuksekCeza(arrCezalar);
                Console.WriteLine("En yüksek cezaIndex: " + cezaIndex);
                CezaIndexHesapla(arrMatrisYedek, cezaIndex, out yatayIndex, out dikeyIndex);

                Console.WriteLine("Yatay Index: " + yatayIndex);
                Console.WriteLine("Dikey Index: " + dikeyIndex);

                Console.WriteLine("Arz : " + arrArzYedek[yatayIndex]);
                Console.WriteLine("Talep: " + arrTalepYedek[dikeyIndex]);

                if (arrArzYedek[yatayIndex] - arrTalepYedek[dikeyIndex] >= 0)
                {
                    Console.WriteLine("Arz - Talep : " + (arrArzYedek[yatayIndex] - arrTalepYedek[dikeyIndex]));
                    arrArzYedek[yatayIndex] = arrArzYedek[yatayIndex] - arrTalepYedek[dikeyIndex];

                    toplamMaliyet += arrMatris[yatayIndex, dikeyIndex] * arrTalepYedek[dikeyIndex];
                    sToplamMaliyet += " +" + arrMatris[yatayIndex, dikeyIndex] + " * " + arrTalepYedek[dikeyIndex];

                    Console.WriteLine("Dikeyden alması gereken: " + arrTalepYedek[dikeyIndex]);
                    Console.WriteLine("Maliyet Hesabı: " + arrMatris[yatayIndex, dikeyIndex] + " * " + arrTalepYedek[dikeyIndex]);

                    arrTalepYedek[dikeyIndex] = 0;
                    sumArz -= arrTalepYedek[dikeyIndex];
                    sumTalep -= arrTalepYedek[dikeyIndex];



                }
                else if (arrTalepYedek[dikeyIndex] - arrArzYedek[yatayIndex] >= 0)
                {
                    Console.WriteLine("Talep - Arz : " + (-arrArzYedek[yatayIndex] + arrTalepYedek[dikeyIndex]));

                    toplamMaliyet += arrMatris[yatayIndex, dikeyIndex] * arrArzYedek[yatayIndex];
                    sToplamMaliyet += " +" + arrMatris[yatayIndex, dikeyIndex] + " * " + arrArzYedek[yatayIndex];

                    Console.WriteLine("Yataydan alması gereken: " + arrArzYedek[yatayIndex]);
                    Console.WriteLine("Maliyet Hesabı: " + arrMatris[yatayIndex, dikeyIndex] + " * " + arrArzYedek[yatayIndex]);

                    arrTalepYedek[dikeyIndex] = arrTalepYedek[dikeyIndex] - arrArzYedek[yatayIndex];
                    arrArzYedek[yatayIndex] = 0;
                    sumArz -= arrArzYedek[yatayIndex];
                    sumTalep -= arrArzYedek[yatayIndex];

                }

                if (arrArzYedek[yatayIndex] == 0)
                {
                    Console.WriteLine(yatayIndex + " için arz bitti değerler yok ediliyor");
                    for (int i = 0; i < sutun; i++)
                        arrMatrisYedek[yatayIndex, i] = int.MaxValue;
                }
                if (arrTalepYedek[dikeyIndex] == 0)
                {
                    Console.WriteLine(dikeyIndex + " için talep bitti değerler yok ediliyor");
                    for (int i = 0; i < satir; i++)
                        arrMatrisYedek[i, dikeyIndex] = int.MaxValue;
                }


                SatirCezalariHesapla(arrMatrisYedek).CopyTo(arrCezalar, 0);
                SutunCezalariHesapla(arrMatrisYedek).CopyTo(arrCezalar, satir);
                Console.WriteLine("Matris:");
                MatrisYazdir(arrMatrisYedek);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Arzlar: ");
                MatrisYazdir(arrArzYedek);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Talepler: ");
                MatrisYazdir(arrTalepYedek);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Cezalar: ");
                MatrisYazdir(arrCezalar);

                Console.WriteLine("Anlık Toplam Maliyet: " + toplamMaliyet);
                index--;

            }
            Console.WriteLine("Toplam Maliyet : " + toplamMaliyet);
            return "Bu yönteme göre en düşük taşıma maliyeti = " + sToplamMaliyet + " = " + toplamMaliyet + " olarak bulunur.\n";
        }

        private bool Cezalar0Mi(int[] arrCezalar)
        {
            for (int i = 0; i < arrCezalar.Length; i++)
                if (arrCezalar[i] != 0)
                  return  false;
            return true;
        }

        private void MatrisYazdir(int[] arrCezalar)
        {
            string x = "";
            for (int col = 0; col < arrCezalar.Length; col++)
                x = x + String.Format("{0}\t", arrCezalar[col]);
            Console.WriteLine(x);
        }

        private void MatrisYazdir(int[,] arrMatrisYedek)
        {
            var rowCount = arrMatrisYedek.GetLength(0);
            var colCount = arrMatrisYedek.GetLength(1);
            for (int row = 0; row < rowCount; row++)
            {
                string x = "";
                for (int col = 0; col < colCount; col++)
                    x = x + String.Format("{0}\t", arrMatrisYedek[row, col]);
                Console.WriteLine(x);
            }
        }

        private void CezaIndexHesapla(int[,] arrMatrisYedek, int cezaIndex, out int yatayIndex, out int dikeyIndex)
        {
            yatayIndex = -1;
            dikeyIndex = -1;
            if(cezaIndex < satir)
            {
                yatayIndex = cezaIndex;
                dikeyIndex = DikeyIndexHesapla(arrMatrisYedek, yatayIndex);
            }
            else
            {
                dikeyIndex = cezaIndex - satir;
                yatayIndex = YatayIndexHesapla(arrMatrisYedek, dikeyIndex);
            }
        }

        private int YatayIndexHesapla(int[,] arrMatrisYedek, int dikeyIndex)
        {
            int ek = arrMatrisYedek[0, dikeyIndex];
            int index = 0;
            for (int i = 1; i < satir; i++)
                if (arrMatrisYedek[i, dikeyIndex] < ek)
                {
                    index = i;
                    ek = arrMatrisYedek[i, dikeyIndex];
                }
            return index;
        }

        private int DikeyIndexHesapla(int[,] arrMatrisYedek, int yatayIndex)
        {
            int ek = arrMatrisYedek[yatayIndex, 0];
            int index = 0;
            for (int i = 1; i < sutun; i++)
                if (arrMatrisYedek[yatayIndex, i] < ek)
                {
                    index = i;
                    ek = arrMatrisYedek[yatayIndex, i];
                }
            return index;
        }

        private int CezadanYerBul(int cezaIndex, out bool cezaYeri)
        {
            cezaYeri = cezaIndex > satir ? false : true;

            return cezaYeri ? cezaIndex : satir + sutun - cezaIndex;

        }

        private int YeriniBul(bool cezaYeri, int cezaIndex)
        {
            return 0;
        }

        /*private bool CezadanYerBul(int cezaIndex)
        {
            return cezaIndex > satir ? false : true;
        }*/

        private int EnYuksekCeza(int[] arrCezalar)
        {
            int cezaIndex = 0;
            int minCeza = arrCezalar[0];
            for (int i = 1; i < arrCezalar.Length; i++)
                if (arrCezalar[i] > minCeza)
                {
                    cezaIndex = i;
                    minCeza = arrCezalar[i];
                }
            return cezaIndex;
        }

        private int[] SutunCezalariHesapla(int[,] arrMatrisYedek)
        {
            int[] sutunCezalar = new int[sutun];
            int ek1 = int.MaxValue, ek2 = int.MaxValue;

            for (int i = 0; i < sutun; i++)
            {
                ek1 = int.MaxValue;
                ek2 = int.MaxValue;
                for (int j = 0; j < satir; j++)
                {
                    if (arrMatrisYedek[j, i] < ek1)
                    {
                        ek2 = ek1;
                        ek1 = arrMatrisYedek[j, i];
                        continue;
                    }
                    if (arrMatrisYedek[j, i] < ek2)
                        ek2 = arrMatrisYedek[j, i];
                }
                sutunCezalar[i] = ek2 - ek1;
                if (ek2 == int.MaxValue)
                    sutunCezalar[i] = ek1;
                if (ek2 == int.MaxValue && ek1 == int.MaxValue)
                    sutunCezalar[i] = 0;
                //Console.WriteLine("Sutün Ceza: " + sutunCezalar[i]);
            }

            return sutunCezalar;
        }

        private int[] SatirCezalariHesapla(int[,] arrMatrisYedek)
        {
            int[] satirCezalar = new int[satir];
            int ek1 = int.MaxValue, ek2 = int.MaxValue;

            for (int i = 0; i < satir; i++)
            {
                ek1 = int.MaxValue;
                ek2 = int.MaxValue;
                for (int j = 0; j < sutun; j++)
                {
                    if (arrMatrisYedek[i, j ] < ek1)
                    {
                        ek2 = ek1;
                        ek1 = arrMatrisYedek[i, j];
                        continue;
                    }
                    if (arrMatrisYedek[i, j] < ek2)
                        ek2 = arrMatrisYedek[i, j];
                }
                satirCezalar[i] = ek2 - ek1;
                if (ek2 == int.MaxValue)
                    satirCezalar[i] = ek1;
                if (ek2 == int.MaxValue && ek1 == int.MaxValue)
                    satirCezalar[i] = 0;
                //Console.WriteLine("Satır Ceza: "+ satirCezalar[i]);
            }

            return satirCezalar;
        }

        public int[,] getMatris()
        {
            return arrMatris;
        }
    }
}
