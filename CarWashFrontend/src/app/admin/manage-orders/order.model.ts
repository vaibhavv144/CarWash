export interface Order {
  id: number;
  userId: string;
  userName: string;
  washerId: string | null;
  washerName: string | null;
  carId: number;
  packageId: number;
  promoCodeId: number;
  scheduledDate: string;
  status: string;
  totalAmount: number;
}
