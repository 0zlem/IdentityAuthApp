import {
  createBrowserRouter,
  RouterProvider,
  redirect,
} from "react-router-dom";
import Home from "./pages/Home";
import Register from "./pages/Register";
import { Toaster } from "sonner";
import Roles from "./pages/Roles";

const authLoader = async () => {
  const token = localStorage.getItem("token");
  const role = localStorage.getItem("role");
  // const fullName = localStorage.getItem("fullName");
  if (!token) throw redirect("/register");
  return { token, role };
};

const adminLoader = async () => {
  const token = localStorage.getItem("token");
  const role = localStorage.getItem("role");
  // const fullName = localStorage.getItem("fullName");
  if (!token || role !== "Admin") throw redirect("/");
  return { token, role };
};

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
    loader: authLoader,
  },
  {
    path: "/register",
    element: <Register />,
  },
  {
    path: "/roles",
    element: <Roles />,
    loader: adminLoader,
  },
]);

function App() {
  return (
    <>
      <Toaster richColors position="top-right" />
      <RouterProvider router={router} />
    </>
  );
}

export default App;
