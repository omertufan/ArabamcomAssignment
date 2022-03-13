# ArabamcomAssignment

Projeyi Çalıştırmak için aşağıdaki araçlara ihtiyacınız olacak:

* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 5 or later](https://dotnet.microsoft.com/download/dotnet-core/5)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

Geliştirme ortamınızı kurmak için aşağıdaki adımları izlemelisiniz:

1-) Çalışmadan Önce Docker Masaüstünü Başlatın

2-) Repoyu klonlayın veya indirin

3-) docker-compose.yml dosyalarının bulunduğu kök dizinde aşağıdaki komutu çalıştırın:

```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

Not: Bağlantı zaman aşımı hatası alırsanız Mac için Docker, lütfen Docker'ın "Deneysel Özelliklerini" Kapatın.

4-) Docker'ın api servis hizmetini ve bağımlı olduğu mongodb, rabbitmq gibi servisleri oluşturmasını bekleyin. (İlk docker imaj kurulum işlemlerinden dolayı bu süreç biraz uzun sürebilir.)

5-) Docker üzerinde tüm hizmetler çalışır duruma geldiğinde Advert apisini aşağıdaki url'den başlatabilirsiniz:

**Advert API'si -> http://localhost:8000/swagger/index.html**
