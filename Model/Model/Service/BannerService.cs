using Microsoft.Extensions.Logging;

namespace My.Demo.FileUpload.Model
{
    public class BannerService : IBannerService
    {
        private readonly ILogger<BannerService> _logger;
        private readonly IBannerRepository _repository;

        public BannerService(ILogger<BannerService> logger, IBannerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public List<Banner> GetList(BannerFilter filter, ResultPaging paging)
        {
            const string func = "GetList";
            try
            {
                return _repository.GetList(filter, paging);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{func}: Exception caught.", func);
                throw;
            }
        }

        public Banner GetData(int bannerId)
        {
            const string func = "GetData";
            try
            {
                return _repository.GetData(bannerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{func}: Exception caught with banner id {bannerId}.", func, bannerId);
                throw;
            }
        }

        public Banner SaveData(Banner banner)
        {
            const string func = "SaveData";
            try
            {
                return _repository.SaveData(banner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{func}: Exception caught.", func);
                throw;
            }
        }

        public bool DeletData(int bannerId)
        {
            const string func = "DeletData";
            try
            {
                return _repository.DeletData(bannerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{func}: Exception caught with banner id {bannerId}.", func, bannerId);
                throw;
            }
        }
    }
}
