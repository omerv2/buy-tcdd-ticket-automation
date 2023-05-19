# Buy.Tcdd.Ticket - Proje Hakkında

Bu proje hızlı tren bileti bulurken harcanan zamanı azaltarak, insanların hayatını kolaylaştırmak için geliştirilmiştir. Hızlı tren biletini konfigürasyonda verilen yolcu ve kart bilgileri doğrultusunda sizin adınıza satın alım yapmaya çalışan ve 3d secure ekranına kadar ilerletir.
Siz de size 3d sms'i gelene kadar çayınızı kahvenizi içebilirsiniz. SMS geldiğinde de o bilgiyi girerek biletinizi satın alabilirsiniz.

## Kullandığı teknoloji

- [.NET Core 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## Ön Gereksinim

- [.NET Core 6.0](https://dotnet.microsoft.com/download/dotnet/6.0).

# Başlarken

Proje selenium driver'larını kullanarak tcdd sitesini ziyaret eder, istediğiniz saatte ve günde bilet bulana kadar tarama yapmaya devam eder. Hata aldığı takdirde tekrar baştan başlar.
Bunların gerçekleşebilmesi için, örnek konfigürasyon dosyasının kopyalanıp sonundaki sample uzantısının silinmesi gerekmektedir. İçerisinde konfigürasyonu kendinize ilgili kişiye göre ayarlayarak ilerleyebilirsiniz.

### 1. Repository nasıl klonlanır?

```
git clone git@github.com:omerv2/buy-tcdd-ticket-automation.git
```

## Projenin ayaklandırılıp çalıştırılması

### 1. Projenin kodlarını indirin

```
git clone git@github.com:omerv2/buy-tcdd-ticket-automation.git
```

### 2. `.env.sample` Dosyasının uzantısını değiştirip `.env` haline getirdikten sonra içerisindeki bilgileri kendinize göre doldurmanız gerekmekte.

 `NEREDEN` alanı trene nereden bineceğinizi belirlemek için gereklidir. Bu liste TCDD'nin kendi listesinde geçen text alanlarının birebir aynılarıdır. Örnek: Arifiye

 `NEREYE` alanı trenden nerede ineceğinizi belirlemek için gereklidir Örnek: İstanbul(Söğütlüçeşme)

 `GIDIS_TARIH`  otomasyonun hangi tarihte gideceğiniz yeri seçeceğine karar vermek için kullanılır. Örnek: 21.05.2023 Format: DD.MM.YYYY

 `SAAT`  otomasyonun hangi saatte gideceğiniz yeri seçeceğine karar vermek için kullanılır, Bu bilgi saat kolonunda nasıl yazılıyorsa öyle konulmalıdır. Örnek: 09:00

 `CEP_TEL`  cep telefonu alanını doldurmak için kullanılır. Örnek: 0533XXXYYYY 

 `EMAIL`  email alanını doldurmak için kullanılır. Örnek: ILGILIMAIL@HOTMAIL.COM

`YOLCU_ISIM`  Treni kullanacak yolcu adı kısmını doldurmak için gereklidir.

`YOLCU_SOYISIM`  Treni kullanacak yolcu soyadı kısmını doldurmak için gereklidir.

`CINSIYET`  Treni kullanacak yolcunun cinsiyet seçimini yapmak için gereklidir. Seçenekler: E YA DA K

`YOLCU_TC`  Treni kullanacak yolcunun tc kn alanını doldurmak için gereklidir.

`YOLCU_DOGUM_TARIHI`  Treni kullanacak yolcunun doğum tarihi alanını doldurmak  için gereklidir. Format: GG/MM/YYYY Örnek: 21/05/1980

`KKART_ISIMSOYISIM`  Kredi kartı isim soyisim alanını doldurmak için gereklidir.

`KKART_NO`  Kredi kartı numara alanını doldurmak için gereklidir.

`KKART_CVC`  Kredi kartı cvc alanını doldurmak için gereklidir.

`KKART_SKT_AY`  Kredi kartı son kullanma tarihi ay alanını doldurmak için gereklidir. Format: XX, Örnek: 07

`KKART_SKT_YIL`  Kredi kartı son kullanma tarihi yıl alanını doldurmak için gereklidir. Format: XX, Örnek: 27


### 3. Projeyi çalıştırmak

Doğru path'te olduğunuzdan emin olduktan sonra (`~\Buy.Tcdd.Ticket`) ->
```sh
dotnet run
```


# Katkı
Proje ihtiyaca binaen geliştirilmiştir. Paylaşılmasının/değiştirilip kullanılmasının herhangi bir mahsuru yoktur.
İşinize yarayacağını düşündüğünüz feature'ları ekleyebilirsiniz.

1. Projeyi klonlayın
2. Main branch'ından pull yapın
3. Kendinize checkout yapacağınız bir oluşturun feature branch: `git checkout -b feature/name-of-the-feature`
4. Değişiklikleri push'layın
5. Main'e PR açın
