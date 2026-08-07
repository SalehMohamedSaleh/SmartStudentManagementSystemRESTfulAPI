using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

public class ImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> SaveImageAsync(IFormFile imageFile, string folderName)
    {
        if (imageFile == null || imageFile.Length == 0)
            throw new ArgumentException("Image file is required.");

        // تحقق من النوع
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            throw new ArgumentException($"Invalid image format. Allowed: {string.Join(", ", allowedExtensions)}");


        // تحقق من الحجم
        const long maxFileSize = 5 * 1024 * 1024; // 5MB
        if (imageFile.Length > maxFileSize)
            throw new ArgumentException($"Image size cannot exceed 5MB.");

        // تحديد مسار مجلد wwwroot
        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        // إنشاء اسم فريد للصورة لمنع التعارض
        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        // حفظ الملف
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }

        // إرجاع المسار النسبي ليتم حفظه في قاعدة البيانات
        return $"/{folderName}/{uniqueFileName}";
    }

    public void DeleteImage(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            return;

        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl.TrimStart('/'));

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}