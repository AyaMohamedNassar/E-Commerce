# Trendily Shop - A Comprehensive eCommerce Solution

**Overview:**

Trendily Shop is a dynamic and user-friendly eCommerce platform designed to cater to both customers and administrators. This project exemplifies modern web development practices, utilizing a robust backend built with .NET Core Web API and MSSQL Server, and a responsive frontend developed with Angular. The system supports two primary user roles: **Customer** and **Admin**, each with distinct capabilities tailored to their needs.

## Key Features

### Customer Features
1. **Product Browsing and Cart Management:**
   - Customers can browse through a variety of products available on the platform. Each product is displayed with detailed information, ensuring that users can make informed purchasing decisions.
   - The shopping cart functionality allows customers to add products, adjust quantities, and manage their selections effortlessly. The cart's state is preserved, providing a seamless shopping experience.

2. **Pagination:**
   - The product list is equipped with pagination to enhance the browsing experience, especially when dealing with a large inventory. This feature ensures that the platform remains responsive and user-friendly, even as the number of products grows.

3. **Security and User Authentication:**
   - Trendily Shop employs JWT (JSON Web Tokens) for secure user authentication. This ensures that customers' data and transactions are protected, providing peace of mind while shopping.

### Admin Features
1. **CRUD Operations for Product Management:**
   - Administrators have full control over the product inventory. They can perform Create, Read, Update, and Delete (CRUD) operations on products. This allows for easy management of the product catalog, enabling the admin to keep the offerings up-to-date and relevant.

2. **N-Tier Architecture and Design Patterns:**
   - The backend of Trendily Shop is built using an N-Tier architecture, ensuring that the application is scalable, maintainable, and easy to extend. This architecture separates concerns across different layers, such as the Data Access Layer (DAL), Business Logic Layer (BLL), and Presentation Layer.
   - The Repository Pattern is implemented to abstract data access, promoting a clean separation of concerns and making the codebase more modular and testable.
   - The Unit of Work pattern is used to manage transactions across multiple repositories, ensuring that changes to the database are consistent and reliable.

3. **Security Measures:**
   - The admin section is protected with role-based access control, ensuring that only authorized personnel can manage the product catalog and perform other administrative tasks.

## Technologies and Frameworks

### Backend (API)
- **.NET Core Web API:** The core technology powering the backend, chosen for its performance, scalability, and robust ecosystem.
- **MSSQL Server:** A powerful relational database management system used to store and manage the application's data.
- **MS Identity:** Integrated for handling user authentication and authorization, providing a secure environment for both customers and admins.
- **JWT (JSON Web Tokens):** Used for securing API endpoints and ensuring that only authenticated users can access specific resources.
- **N-Tier Architecture:** The application is structured into multiple layers (Presentation, Business Logic, Data Access), which promotes separation of concerns and enhances maintainability.
- **Repository Pattern:** Used to abstract the data layer, making the application more modular and easier to manage.
- **Unit of Work:** Ensures that all database operations are performed in a consistent and reliable manner, managing transactions across repositories.

### Frontend (Client-Side)
- **Angular:** A powerful front-end framework used to build the client side of the application, providing a responsive and dynamic user interface.
- **Route Guard:** Implemented to protect certain routes, ensuring that only authenticated users can access specific parts of the application.
- **Interceptor for Token Handling:** An Angular interceptor is maintained to automatically attach JWT tokens to outgoing HTTP requests, ensuring secure communication with the API.
- **Bootstrap:** Used to create a responsive and modern user interface, enhancing the overall user experience.

## Project Structure

- **EcommerceAPI:** This directory contains the backend API built with .NET Core and MSSQL Server. It handles all the server-side logic, including data management, business rules, and API endpoints.
- **TrendilyShop:** This directory contains the client-side application built with Angular. It manages the user interface, handling all interactions between the customer and the admin.

## Development Practices

- **Version Control:** The project is managed with Git, ensuring that all changes are tracked, and the codebase remains clean and organized.
- **Testing:** Both the frontend and backend are rigorously tested to ensure that the application is reliable and free of bugs.

## Conclusion

Trendily Shop is more than just an eCommerce platform; it is a demonstration of modern web development practices. By leveraging .NET Core, MSSQL Server, Angular, and a suite of design patterns and best practices, this project delivers a robust, scalable, and user-friendly solution for online shopping. Whether you're a customer looking for a seamless shopping experience or an admin managing a dynamic product catalog, Trendily Shop is designed to meet your needs with efficiency and elegance.
