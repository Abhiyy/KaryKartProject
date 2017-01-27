﻿ALTER TABLE ProductShipping
ADD FOREIGN KEY (ProductID)
REFERENCES Product(ProductID)

ALTER TABLE Product
ADD FOREIGN KEY (CategoryID)
REFERENCES Category(CategoryID)

ALTER TABLE Product
ADD FOREIGN KEY (BrandID)
REFERENCES Brand(BrandID)

ALTER TABLE Product
ADD FOREIGN KEY (SubCategoryID)
REFERENCES SubCategory(SCategoryID)

ALTER TABLE ProductFeature
ADD FOREIGN KEY (ProductID)
REFERENCES Product(ProductID)

ALTER TABLE ProductImage
ADD FOREIGN KEY (ProductID)
REFERENCES Product(ProductID)

ALTER TABLE ProductPrice
ADD FOREIGN KEY (ProductID)
REFERENCES Product(ProductID)

ALTER TABLE ProductSizeMapping
ADD FOREIGN KEY (ProductID)
REFERENCES Product(ProductID)