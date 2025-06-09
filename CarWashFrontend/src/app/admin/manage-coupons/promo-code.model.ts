export interface PromoCode {
  id?: number;
  code: string;
  discountPercent: number;
  isActive?: boolean;
  validTill: string; // Matches .NET backend property
}
