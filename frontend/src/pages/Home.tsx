import { Link, useNavigate } from "react-router-dom";
import { Button } from "../components/ui/button";
import { logout } from "../services/AuthService";

function Home() {
  const navigate = useNavigate();

  const handleLogout = async () => {
    await logout();
    await navigate("/register", { replace: true });
  };
  return (
    <div className="flex gap-2 justify-center items-center ">
      <Link to={"/"}>
        <Button
          className="cursor-pointer bg-blue-600 text-white hover:bg-blue-700"
          variant={"outline"}
        >
          Home
        </Button>
      </Link>
      <Link to={"/roles"}>
        <Button
          className="cursor-pointer bg-blue-600 text-white hover:bg-blue-700"
          variant={"outline"}
        >
          Roles
        </Button>
      </Link>
      <Button
        className="cursor-pointer bg-red-600 text-white hover:bg-red-700"
        onClick={handleLogout}
      >
        Logout
      </Button>
    </div>
  );
}

export default Home;
