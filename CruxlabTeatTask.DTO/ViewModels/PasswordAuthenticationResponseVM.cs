namespace CruxlabTeatTask.DTO.ViewModels
{
    public class PasswordAuthenticationResponseVM
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int ChecksCount { get; set; }
        public int PassedPasswords { get; set; }
    }
}
