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
  if (!token) throw redirect("/register");
  return null;
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
    loader: authLoader,
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
