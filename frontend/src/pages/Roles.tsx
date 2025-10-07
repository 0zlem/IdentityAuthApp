import Home from "./Home";
import AddRole from "../components/AddRole";
import AddUserRole from "../components/AddUserRole";

function Roles() {
  return (
    <div>
      <Home />
      <div className="relative flex flex-row justify-center items-start gap-8 mt-14">
        <div>
          <AddRole />
        </div>
        <div>
          <AddUserRole />
        </div>
      </div>
    </div>
  );
}

export default Roles;
