// src/app/admin/manage-packages/package.model.ts
export interface Package {
  id: number;
  name: string;
  description: string;
  price: number;
  isActive: boolean;
}
