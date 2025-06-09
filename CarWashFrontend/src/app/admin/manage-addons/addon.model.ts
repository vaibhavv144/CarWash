export interface AddOn {
  id: number;
  name: string;
  price: number;
  isActive: boolean;
}

export interface AddOnCreateDto {
  name: string;
  price: number;
  isActive: boolean;
}
