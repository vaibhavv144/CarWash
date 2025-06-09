
public interface IPromoCodeRepository
{
    Task<IEnumerable<PromoCode>> GetAllPromoCodeAsync();
    Task<PromoCode> AddPromoAsync(PromoCode promoCode);
    Task<PromoCode> UpdatePromoAsync(int promocodeId, PromoCode promoCode);

    Task<bool> DeletePromoAsync(int promoCodeId);

}
