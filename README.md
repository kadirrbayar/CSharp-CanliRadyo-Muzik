<h1 align= center><b>⭐️ Canlı Radyo / Müzik Player (C# WinForms) ⭐️</b></h1>
<h3 align = center> Bu uygulama C# ve MySql kullanarak hazırlanmış bir WinForms uygulamasıdır.</h3>

## ✨ <a name="features">Tanıtım</a>

### 📻 7/24 Canlı Radyo

Uygulamaya istediğiniz kadar radyo eklemesi yapabilirsiniz ve sorunsuz olarak dinleyebilirsiniz. M3U8 Formatlı radyolar 'LibVlc' paketi kullanılarak uygulama içerisinde çalışmaktadır.
Admin girişi yaptıktan sonra radyo ekleme menüsüne girebilir ve radyonuzun ismini ve M3U8 linkini girerek veritabanına ekleyebilirsiniz.

### 🎵 Müzik Player

Uygulama yine aynı şekilde müziklerinizi 'LibVlc' paketi ile çalıştırmaktadır. Müzikleri 'Google Drive' paylaşım linki ile çalıştırmaktadır. A
dmin girişi yaptıktan sonra müzik eklemek için müzik ekle menüsüne giriniz. 
Eklemek istediğiniz müziği 'Google Drive' hesabınıza yükleyin.
Google Drive hesabınıza yüklediğiniz müziğin üzerine sağ tıklayarak bağlantıyı kopyala seçeneğine tıklayınız.
Ardından aşağıdaki gibi bir bağlantı linkine sahip olacaksınız. 
"https://drive.google.com/file/d/10vAGMn-YdQd9qaPph_xjyLuxct93FMQP/view?usp=share_link"
Bağlantı linkindeki "file/d/" kısmından sonraki ve "/view?" satırından önceki urlyi kopyalayın.
Şimdi uygulamanıza geri dönebilir ve müzik linkine bu urlyi yapıştırabilirsiniz.
Ardından sanatçı adını, müzik adını ve hangi kategoriye eklemek istediğinizi seçtikten sonra müziğiniz başarıyla veritabanına eklenecektir.

### 🎯 Küçük Boyutlu ve Sade

Uygulama müzikleri ve kategori görsellerini veritabanında barındırmamaktadır. Bu şekilde veritabanınızdan tasarruf edebilirsiniz. 
Müzikleri 'Google Drive' ve görselleri ise 'HizliResim' platformundan almaktadır. Dilerseniz farklı platformları da kullanabilirsiniz. 
Uygulama sade yapısı ise istediğinizi kolayca bulmanıza yardımcı olmaktadır. Kategorize edilmiş yapısı ve arama kutusu ile dilediğiniz müziği ve radyoyu anında bulabilirsiniz.

### 🌄 Uygulama Görselleri

<p align="center">
  <img src="https://telegra.ph/file/58cb9654bec074b1fe104.png">
  <img src="https://telegra.ph/file/04f6a8da4e55a94a3db02.png">
  <img src="https://telegra.ph/file/fea78c1b76eb226a77b1d.png">
  <img src="https://telegra.ph/file/4c66c10eda1675be80950.png">
  <img src="https://telegra.ph/file/714deacaa89142c63fa85.png">
  <img src="https://telegra.ph/file/dcbd5db1c822e9e3d1bd2.png">
  <img src="https://telegra.ph/file/6c0e0ecdca2ff3ee97403.png">
  <img src="https://telegra.ph/file/14120c481f5ff00103260.png">
</p>

## ⚒ <a name="configs">Kurulum</a>

    1-) MySql sistemine girin. Uygulama içerisinde MySql bilgileri "Kullanıcı Adı : root" "Şifre : 12345" olarak belirlenmiştir. 
    Eğer farklı bir kullanıcı adı şifreniz var ise 'connect.cs' içerisinde yer alan bağlantı bilgilerini güncelleyiniz.
    2-) Yeni bir veritabanı oluşturun ve veritabanı ismini '43stationc#' karakter tipini ise 'utf8mb4_unicode_ci' olarak ayarlayın. 
    3-) İçe aktar menüsüne girin ve uygulama paketi içerisinde yer alan 'Database' klasöründeki veritabanı dosyasını seçin. Daha sonra içe aktarmayı onaylayın.
    4-) Veritabanı içerisine tabloların geldiğini doğrulayın.
    5-) Uygulama klasöründe yer alan 'FontPacks' klasöründeki fontları yükleyin.
    6-) Solutionu çalıştırın.
    7-) NuGet Package bölümüne girin ve 'LibVlc Windows' Paketini arayın. Eğer hali hazırda yüklü ise kaldırıp yeniden yükleyin.
    8-) Projenizi derleyip çalıştırın. 
    Admin Bilgileri : "Kullanıcı Adı : admin" "Şifre : 12345" olarak belirlenmiştir.

## 📃 <a name="license">Lisans</a>

 - Copyright (C) 2023-Present by [kadir](github.com/kadir008) ❤️️
 - Licensed under the terms of the [GNU GENERAL PUBLIC LICENSE](https://github.com/kadir008/Muzik-ve-Radyo-Uygulamasi/blob/main/LICENSE)
