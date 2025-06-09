using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PromoCodeRepository : IPromoCodeRepository
{
    private readonly CarWashContext _context;

    public PromoCodeRepository(CarWashContext carwash)
    {
        _context = carwash;
    }

    public async Task<IEnumerable<PromoCode>> GetAllPromoCodeAsync()
    {
        return await _context.PromoCodes.ToListAsync();
    }

    public async Task<PromoCode> AddPromoAsync(PromoCode promoCode)
    {
        _context.PromoCodes.Add(promoCode);
        await _context.SaveChangesAsync();
        return promoCode;
    }

    public async Task<PromoCode> UpdatePromoAsync(int promocodeId, PromoCode promoCode)
    {
        var existingPromo = await _context.PromoCodes.FindAsync(promocodeId);
        if (existingPromo == null)
        {
            return null;
        }
        existingPromo.Code=promoCode.Code;
        existingPromo.DiscountPercent = promoCode.DiscountPercent;
        existingPromo.ValidTill = promoCode.ValidTill;
        await _context.SaveChangesAsync();
        return existingPromo;
    }
    public async Task<bool> DeletePromoAsync(int promoCodeId)
    {
        var promo = await _context.PromoCodes.FindAsync(promoCodeId);
        if (promo == null) return false;

        _context.PromoCodes.Remove(promo);
        await _context.SaveChangesAsync();
        return true;
    }

}
