### What is CQS Design Pattern?
1. CQS (Command-Query Separation) is a software design pattern that separates operations that change the state of an object (commands) from operations that return information about the state of an object (queries). The goal of this pattern is to make it clear which operations will change the state of an object and which will not, which can help to prevent bugs and make the code more maintainable.

2. The CQS pattern is based on the idea that commands should be designed to be idempotent and queries should be designed to be pure. Idempotent commands can be called multiple times without changing the state of the object beyond the first call, and pure queries are functions that return the same result for the same input and don't cause any side effects.

3. In practice, this means that commands should be named with verbs, such as "save", "update", "delete", and queries should be named with nouns, such as "get" or "find".

4. An example of CQS pattern in practice is in a CRUD application, where the commands would handle the "create", "update" and "delete" operations and the queries would handle the "read" operation.

5. CQS pattern is commonly used in functional programming and can be seen as a way of structuring commands and queries in a way that makes it clear which operations will change the state of an object and which will not.

### What is the Example of CQS Pattern?
1. NgRx is a library for managing state in Angular applications, and it can be used as an example of the CQS pattern.
2. NgRx uses the Redux pattern, which is based on the CQS pattern, to manage state. The state is stored in a single store, and the store can only be updated by dispatching actions, which are considered commands. The state can be read by selecting state from the store, which is considered a query.

### CQS Example with CRUD?
```javascript
class CustomerService {
  private customers: Customer[] = [];

  createCustomer(customer: Customer): void {
    this.customers.push(customer);
  }

  updateCustomer(customer: Customer): void {
    const index = this.customers.findIndex(c => c.id === customer.id);
    this.customers[index] = customer;
  }

  deleteCustomer(id: string): void {
    this.customers = this.customers.filter(c => c.id !== id);
  }

  getCustomer(id: string): Customer | undefined {
    return this.customers.find(c => c.id === id);
  }

  getCustomers(): Customer[] {
    return this.customers;
  }
}

```
### CQS Example with NgRx?
1. Create
```typescript
import { createAction, props } from '@ngrx/store';

export const addProduct = createAction(
  '[Cart] Add Product',
  props<{ productId: number }>()
);
```
2. Getting
```typescript
import { createReducer, on } from '@ngrx/store';
import { addProduct } from './cart.actions';

export interface CartState {
  products: number[];
}

export const initialState: CartState = {
  products: []
};

export const cartReducer = createReducer(
  initialState,
  on(addProduct, (state, { productId }) => {
    return { ...state, products: [...state.products, productId] };
  })
);
```
3. Registering in Module
```typescript
import { StoreModule } from '@ngrx/store';
import { cartReducer } from './cart.reducer';

@NgModule({
  imports: [StoreModule.forRoot({ cart: cartReducer })],
  providers: []
})
export class AppModule {}
```
4. Utilizing in the Components
```typescript
import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { addProduct } from './cart.actions';

@Component({
  selector: 'app-root',
  template: `
    <button (click)="addProductToCart(1)">Add Product to Cart</button>
    <div *ngFor="let productId of products">{{productId}}</div>
  `
})
export class AppComponent {
  products: number[];

  constructor(private store: Store) {
    store.select(state => state.cart.products).subscribe(products => {
      this.products = products;
    });
  }

  addProductToCart(productId: number) {
    this.store.dispatch(addProduct({ productId }));
  }
}
```