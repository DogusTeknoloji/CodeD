# Toplantı Notları: TM - DDD Proje Geliştirme Alt Grup Toplantısı

Toplantı Kaydı : [07.05.2025](https://teams.microsoft.com/l/meetingrecap?driveId=b%21spO-K_0G7ESVrs8bgmu2esJUQIPvlZ5ChEXObQySCdacROzMYkOhRIeAjRY5hyCp&driveItemId=014SDK3LB6WW356HYYDVE3ZXP22DQZG3ZQ&sitePath=https%3A%2F%2Fdogusgrubu-my.sharepoint.com%2F%3Av%3A%2Fg%2Fpersonal%2Falper_konuralp_d-teknoloji_com_tr%2FET61t98fGB1JvN360OGTbzABIZlkbiEwDjelomdTzWUJ4A&fileUrl=https%3A%2F%2Fdogusgrubu-my.sharepoint.com%2F%3Av%3A%2Fg%2Fpersonal%2Falper_konuralp_d-teknoloji_com_tr%2FET61t98fGB1JvN360OGTbzABIZlkbiEwDjelomdTzWUJ4A&iCalUid=040000008200E00074C5B7101A82E00807E90507304651E4DA5CDB0100000000000000001000000072BBB05222EB374490791FC844983BFB&masterICalUid=040000008200E00074C5B7101A82E00800000000304651E4DA5CDB0100000000000000001000000072BBB05222EB374490791FC844983BFB&threadId=19%3Ameeting_ZDkxZWYwNzAtM2U0MC00ZGE3LTg3ZGEtZGM2NjQ3ZTIwNTNk%40thread.v2&organizerId=e445dc53-524a-43e2-8f32-29ceaaaf215a&tenantId=cc76235c-86ab-4979-bc0b-0e78c66edb7c&callId=963543c3-6e8c-4012-a9d7-b240ad858ff8&threadType=Meeting&meetingType=Recurring&subType=RecapSharingLink_RecapChiclet)

## Katılımcılar

* Alper Konuralp (Doğuş Teknoloji)
* Gökhan Atay (Doğuş Teknoloji)
* Hamza Ağar (Doğuş Teknoloji)
* Volkan Keskin (Doğuş Teknoloji)
* Sertay Kabuk (Doğuş Teknoloji)
* Sena Ünalmış (Doğuş Teknoloji)

## Ana Gündem Maddesi

`UpdateCategory` komutu ve ilgili işlevlerin geliştirilmesi, test edilmesi ve iyileştirilmesi.

## Tartışılan Konular ve Alınan Kararlar

1. **`UpdateCategory` Komutunun İncelenmesi ve Geliştirilmesi:**
    * Toplantı, `UpdateCategory` komutunun kaldığı yerden devam etti. Gelen veriyle komutun doldurulması ve bu komutu dinleyen event'lerin bulunması gerektiği belirtildi.
    * `UnitOfWork` kullanımı ve request ID'sinin `null` olup olmamasına göre veri çekme mantığı incelendi.
    * Veri ID veya Key ile çekilebiliyor. Eğer ID varsa ID'den, yoksa Key'den devam ediliyor.
    * `Update` metodunda `title` ve `externalReference` (özellikle `sourceVersion`) değiştirilebiliyor. Bu bilgilerin request içerisinde geldiği varsayıldı.
    * İşlem sonunda `Result<Category>` dönülmesi ve isimlendirmenin `ResultCategory` olarak düzeltilmesi konuşuldu.

2. **Kod İyileştirmeleri ve C# Özellikleri Üzerine Tartışmalar:**
    * `Result.IsSuccess` ve `Result.Value` kullanımı üzerine bir tartışma yaşandı. Hamza Ağar, C# 9.0 ile gelen "is pattern" (`is Success { Value: var category }`) ve "recursive patterns" kullanımını önerdi. Bu sayede hem `null` kontrolü hem de `IsSuccess` durumu tek bir yapıda kontrol edilebiliyor.
    * `and`, `or` gibi pattern matching operatörlerinin kullanımı denendi ve tartışıldı. `and` operatörünün bir objenin birden fazla özelliğini kontrol etmek için mantıklı olduğu, `or` operatörünün ise bu senaryoda doğrudan bir karşılığının olmadığı gözlemlendi.
    * Bu yeni C# özelliklerinin çok fazla olması, dilin karmaşıklaşması ve ekip içinde farklı yazım tarzlarına yol açarak standartlaşmayı zorlaştırması gibi konular ele alındı.

3. **ID ve Key Yönetimi (URL vs. Body):**
    * `UpdateCategory` işlemi için ID'nin hem URL'den hem de request body'sinden gelebilmesi durumu ele alındı.
    * URL'den gelen ID'nin, body'den gelen ID'yi ezmesi (üzerine yazması) gerektiği kararlaştırıldı.
    * Request'teki ID alanı `string` olarak değiştirilip, `Guid.TryParse` ile GUID olup olmadığı kontrol edildi. Eğer parse edilemiyorsa Key olarak, edilebiliyorsa GUID olarak işlenmesi sağlandı.

4. **External Reference Güncelleme Mantığı:**
    * `UpdateCategory` sırasında `externalReference` alanının güncellenmesinde bir hata fark edildi. Eğer request'te `externalReference` bilgileri (Service Provider Key, Turkey MID) gönderilmezse, mevcut değerler yerine `null` değerlerle yeni bir `ExternalReference` oluşturulmaya çalışılıyordu.
    * Çözüm olarak, `Create` işlemindeki mantığa benzer şekilde, eğer request body'sinde `Service Provider Key` ve `Turkey MID` kolonları doluysa yeni bir `ExternalReference` oluşturulması, aksi halde mevcut `ExternalReference`'ın korunması veya `null` olarak atanması sağlandı.

5. **HTTP PUT Metodu ve URL Yapılandırması:**
    * PUT metodunda URL'de ID olmadan istek yapıldığında "Method Not Allowed" hatası alındı.
    * Bunun sebebi, `[HttpPut("{id}")]` attribute'ünün ID'yi zorunlu kılmasıydı.
    * Çözüm olarak, aynı metoda ikinci bir `[HttpPut]` attribute'ü eklenerek (ID parametresi olmadan) hem ID'li hem de ID'siz PUT isteklerinin aynı metoda yönlendirilmesi sağlandı. Bu sayede, ID'siz PUT isteği geldiğinde body içerisindeki Key veya ID (eğer varsa) ile işlem yapılabildi.

6. **Test Senaryoları ve Sonuçları:**
    * **Senaryo 1 (ID ile Güncelleme):** URL'de ID, body'de yeni `title` gönderildi. Başarıyla güncellendi, veritabanında değişiklik gözlendi. `SourceVersion` güncellendi.
    * **Senaryo 2 (Key ile Güncelleme):** URL'de Key, body'de yeni `title` gönderildi. Başarıyla güncellendi, veritabanında değişiklik gözlendi.
    * **Senaryo 3 (URL'de ID/Key olmadan, Body'de Key ile Güncelleme):** Yukarıdaki HTTP PUT metodu düzenlemesi sonrasında bu senaryo da başarıyla çalıştı.

7. **Yan Konular:**
    * **Şifre Yönetimi:** Passkey'ler, Bitwarden, RoboForm gibi şifre yöneticileri ve güvenlik üzerine kısa bir sohbet geçti.
    * **Geliştirici Araçları:** Sertay Kabuk, framework dökümanlarını güncel olarak takip edip Copilot'a entegre eden "Context Scout" adlı bir araçtan bahsetti. Hamza Ağar ise Coursera'daki dökümantasyon linklerini izlediğini belirtti.

## Sonraki Adımlar / Gelecek Toplantı Gündemi

* `Delete` (Silme) işlevselliği üzerinde çalışılacak.
* `Get` (Tekil Kayıt Getirme) ve `List` (Liste Getirme) gibi Query (Sorgu) tarafındaki işlemler geliştirilecek.

## Toplantı Formatı Üzerine Öneri

* Alper Konuralp, toplantıların daha interaktif olması için her hafta farklı bir katılımcının ekran paylaşımı yaparak kodu geliştirmesi önerisinde bulundu. Bu konunun bir sonraki toplantıda tekrar değerlendirilmesi kararlaştırıldı.

## Toplantı Sonu

* Katılımcılar birbirlerine teşekkür ederek toplantıyı sonlandırdı.
