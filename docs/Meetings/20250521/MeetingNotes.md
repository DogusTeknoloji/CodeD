# 21 Mayıs 2025 CodeD Haftalık Toplantı Notları

## Katılımcılar

- Alper Konuralp (Doğuş Teknoloji)
- Gökhan Atay (Doğuş Teknoloji)
- Sertay Kabuk (Doğuş Teknoloji)
- Ali Kayhan (Doğuş Teknoloji)

---

## Gündem ve Öne Çıkan Konular

### 1. Kategori CRUD İşlemleri ve CQRS Uygulaması

- Kategori üzerinde yapılan güncelleme ve silme işlemleri canlı olarak denendi.
- Komut (Command) ve sorgu (Query) ayrımı, MediatR ile uygulandı.
- Silme işlemi için özel bir komut ve handler yazıldı, domain event tetiklenmesi sağlandı.
- Hatalı isteklerde Result tipiyle hata yönetimi uygulandı, exception fırlatılmadı.
- API tarafında 204 No Content ve 404 Not Found gibi uygun HTTP status kodları kullanıldı.
- Kategori aggregate root olarak tanımlandı, DDD prensiplerine uygun ilerleniyor.

### 2. Teknik Sorunlar ve Çözümler

- Konsol uygulamasında select mode ile ilgili bir problem yaşandı, çözümü tartışıldı.
- Request parametrelerinin eksik veya hatalı gönderilmesiyle ilgili örnekler üzerinden gidildi.
- EF Core ile yapılan güncellemelerde, değişikliklerin doğru şekilde yansıyıp yansımadığı kontrol edildi.

### 3. Yapay Zeka ve LLM Üzerine Sohbet

- Büyük dil modellerinin (LLM) mevcut sınırları ve geleceği tartışıldı.
- Model boyutu büyüdükçe cevaplama süresi ve doğruluk oranı üzerine fikir alışverişi yapıldı.
- Anlık öğrenme ve Matrix’teki gibi “anında bilgi yükleme” ihtiyacı vurgulandı.
- Google ve diğer arama motorlarının AI entegrasyonu hakkında deneyimler paylaşıldı.

### 4. Sonraki Adımlar

- Kategori sorgu (Query) tarafının tamamlanması planlandı.
- Kodun başka bir ekip üyesi tarafından da sürdürülebilmesi için yazışma ve dokümantasyon yapılacak.
- Haftaya Elastic eğitiminden dolayı toplantı günü değişebilir, bilgilendirme yapılacak.

---

## Kararlar ve Aksiyonlar

- [ ] Kategori Query handler’ı tamamlanacak.
- [ ] Kodun güncel hali ve toplantı notları ekip ile paylaşılacak.
- [ ] Aggregate root’lar arası iletişim için DDD’ye uygun çözüm önerileri hazırlanacak.
- [ ] Sonraki toplantı tarihi netleştirilecek.

---

## Ek Notlar

- Toplantı boyunca kodun DDD, CQRS ve Result tipiyle hata yönetimi prensiplerine uygunluğu gözden geçirildi.
- Katılımcılar, kodun sürdürülebilirliği ve ekip içi bilgi paylaşımı için dökümantasyonun önemini vurguladı.
- Haftaya Elastic eğitiminden dolayı toplantı günü değişebilir.

---

## Kayıt ve Transcript Linkleri

- 📹 [Toplantı Video Kaydı](https://dogusgrubu-my.sharepoint.com/:v:/r/personal/alper_konuralp_d-teknoloji_com_tr/Documents/Kay%C4%B1tlar/TM%20-%20DDD%20Proje%20Geli%C5%9Ftirme%20Alt%20Grup%20Toplant%C4%B1s%C4%B1-20250521_130834-Toplant%C4%B1%20Kayd%C4%B1.mp4?csf=1&web=1&e=X8NBUJ&nav=eyJyZWZlcnJhbEluZm8iOnsicmVmZXJyYWxBcHAiOiJTdHJlYW1XZWJBcHAiLCJyZWZlcnJhbFZpZXciOiJTaGFyZURpYWxvZy1MaW5rIiwicmVmZXJyYWxBcHBQbGF0Zm9ybSI6IldlYiIsInJlZmVycmFsTW9kZSI6InZpZXcifX0%3D)
- 📝 [Toplantı Transcript Dosyası](./Transcript.vtt)

---

> **Not:** Bu notlar, toplantı sırasında yapılan konuşmaların özetidir. Detaylar ve kod örnekleri için ilgili transcript ve kod dosyalarına başvurabilirsiniz.
