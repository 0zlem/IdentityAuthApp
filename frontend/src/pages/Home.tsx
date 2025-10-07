import { Link, useNavigate } from "react-router-dom";
import { Button } from "../components/ui/button";
import { logout } from "../services/AuthService";

function Home() {
  const navigate = useNavigate();

  const role = localStorage.getItem("role");
  const fullName = localStorage.getItem("fullName");

  const handleLogout = async () => {
    await logout();
    await navigate("/register", { replace: true });
  };

  return (
    <div className="flex justify-between items-center px-4 py-2 shadow-sm">
      <div className="flex gap-2 items-center">
        <Link to={"/"}>
          <Button className="cursor-pointer bg-blue-600 text-white hover:bg-blue-700 border-none">
            Home
          </Button>
        </Link>
        {role === "Admin" && (
          <Link to={"/roles"}>
            <Button className="cursor-pointer bg-blue-600 text-white hover:bg-blue-700 border-none">
              Roles
            </Button>
          </Link>
        )}
      </div>

      <div className="flex gap-2 items-center">
        <Button className="bg-blue-600 text-white">{fullName}</Button>
        <Button
          className="cursor-pointer bg-red-600 text-white hover:bg-red-700"
          onClick={handleLogout}
        >
          Logout
        </Button>
      </div>
    </div>
  );
}

export default Home;
