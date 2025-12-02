# Firmness - Construction Management System (Client Module)

A modern React-based client application for managing construction materials, tools, and vehicle rentals.

## 🚀 Tech Stack

- **Framework**: React 19.2 with Vite
- **Routing**: React Router DOM 7.9
- **Styling**: TailwindCSS 4.1
- **Icons**: Lucide React
- **HTTP Client**: Axios
- **Authentication**: JWT (JSON Web Tokens)
- **Testing**: Vitest + React Testing Library

## 📋 Features

- **User Authentication**: JWT-based login and registration
- **Product Catalog**: Browse construction tools, machinery, and vehicles
- **Shopping Cart**: Add items, calculate totals with tax (19%)
- **Demo Mode**: Test the application without backend connection
- **Responsive Design**: Mobile-first, adaptive UI
- **Search**: Filter products by name or SKU

## 🛠️ Installation

### Prerequisites
- Node.js 18+ and npm

### Local Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd firmness.Client
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Configure environment** (optional)
   Create a `.env` file if you need to connect to a backend API:
   ```env
   VITE_API_URL=http://localhost:5042
   ```

4. **Run the development server**
   ```bash
   npm run dev
   ```
   The application will be available at `http://localhost:5173`

## 🐳 Docker Setup (Optional)

Build and run with Docker:

```bash
docker build -t firmness-client .
docker run -p 5173:5173 firmness-client
```

## 🧪 Testing

Run unit tests:
```bash
npm test
```

Run tests with coverage:
```bash
npm run test:coverage
```

## 📁 Project Structure

```
firmness.Client/
├── src/
│   ├── api/              # API client and auth services
│   ├── components/       # Reusable components (Layout, etc.)
│   ├── context/          # React Context (CartContext)
│   ├── pages/            # Page components (Login, Products, Cart, Register)
│   ├── tests/            # Unit tests
│   ├── App.jsx           # Main app component with routing
│   ├── main.jsx          # Application entry point
│   └── index.css         # Global styles
├── public/               # Static assets
├── Dockerfile            # Docker configuration
├── vite.config.js        # Vite configuration
└── package.json          # Dependencies and scripts
```

## 🔑 Demo Mode

To test the application without a backend:

1. Navigate to the login page
2. Click **"Demo Login"** button
3. You'll be redirected to the products page with mock data

## 🌐 API Integration

The client consumes the following endpoints from the Firmness API:

- `POST /api/Auth/register` - User registration
- `POST /api/Auth/login` - User authentication
- `GET /api/Products` - Fetch products (requires JWT)
- `POST /api/Sales` - Create a sale (requires JWT)

All authenticated requests include the JWT token in the `Authorization` header:
```
Authorization: Bearer <token>
```

## 👥 User Roles

The client module is designed for the **Client** role with access to:
- Product catalog
- Shopping cart
- Purchase history
- Profile management

Admin endpoints are not accessible from this frontend.

## 🎨 Design System

- **Primary Color**: Orange (#F97316)
- **Typography**: Inter font family
- **Components**: Custom-designed with TailwindCSS
- **Icons**: Lucide React icon library

## 📝 Available Scripts

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run preview` - Preview production build
- `npm run lint` - Run ESLint
- `npm test` - Run unit tests

## 🔒 Security

- JWT tokens stored in `localStorage`
- Automatic token expiration handling
- Protected routes with authentication checks
- HTTPS recommended for production

## 📄 License

GNU General Public License v3.0 - See LICENSE file for details

## 👨‍💻 Development

Built with ❤️ for construction management and material procurement.
