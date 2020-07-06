# freddytoolapi

This tool is going to support sales department in case of individual cases of customers needs and will handle the state of the requested items

- Product is basically the item on stock
  - name
  - desc
  - size
  - qtty
  - picture
  - link to item on stock
  
- Customer/Person is an object that holds all necessary informations for quick identification of who the order belongs to
  - Name
  - Email address
  - Phone number
  
- Order is
  - list of items that customer wants to order
  - additional informations like 
    - Date, 
    - Name of the order, 
    - Description of the Order, 
    - Note
  - association with the Person / Customer
  - status of the order (solved, unprocessable, cancelled...)
  
- OrderItem is an item in the Order with a specific price, note, status, etc.
  - status of the item (unorderable, ordered...)
  - price
  - note
  
