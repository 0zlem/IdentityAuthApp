import { Button } from "../components/ui/button";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "../components/ui/card";
import { Input } from "../components/ui/input";
import { Label } from "../components/ui/label";
import Home from "./Home";

function Roles() {
  return (
    <div>
      <Home />
      <div className="flex flex-row justify-center items-center gap-4 mt-14">
        <Card className="w-full max-w-sm shadow-2xl border-none text-white">
          <CardHeader>
            <CardTitle className="text-center">Add Role</CardTitle>
          </CardHeader>
          <CardContent>
            <form>
              <div className="grid gap-2">
                <Label htmlFor="email">Role Name</Label>
                <Input type="text" required />
              </div>
            </form>
          </CardContent>
          <CardFooter className="flex-col gap-2">
            <Button
              type="submit"
              className="w-full bg-blue-700 hover:bg-blue-900"
            >
              Add
            </Button>
          </CardFooter>
        </Card>
        <Card className="w-full max-w-sm shadow-2xl border-none text-white">
          <CardHeader>
            <CardTitle className="text-center">Delete Role</CardTitle>
          </CardHeader>
          <CardContent>
            <form>
              <div className="grid gap-2"></div>
            </form>
          </CardContent>
          <CardFooter className="flex-col gap-2">
            <Button
              type="submit"
              className="w-full bg-blue-700 hover:bg-blue-900"
            >
              Add
            </Button>
          </CardFooter>
        </Card>
      </div>
    </div>
  );
}

export default Roles;
