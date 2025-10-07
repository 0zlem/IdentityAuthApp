import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import z from "zod";
import { addUserRole, getRoles, getUsers } from "../services/AuthService";
import { toast } from "sonner";
import { Card, CardContent, CardHeader, CardTitle } from "./ui/card";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "./ui/form";
import { Button } from "./ui/button";
import { useEffect, useState } from "react";

const formSchema = z.object({
  roleId: z.string().min(1, { message: "Please select a role" }),
  userId: z.string().min(1, { message: "UserId is required" }),
});

function AddUserRole() {
  const [roles, setRoles] = useState<{ id: string; name: string }[]>([]);
  const [users, setUsers] = useState<{ id: string; userName: string }[]>([]);

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      roleId: "",
      userId: "",
    },
  });

  useEffect(() => {
    const fetchRoles = async () => {
      try {
        const response = await getRoles();
        setRoles(response.data || response);
      } catch (error) {
        console.error(error);
        toast.error("Failed to get roles");
      }
    };

    const fetchUsers = async () => {
      try {
        const response = await getUsers();
        setUsers(response.data || response);
      } catch (error) {
        console.log(error);
        toast.error("Failed to get users");
      }
    };
    fetchRoles();
    fetchUsers();
  }, []);

  const onSubmit = async (values: z.infer<typeof formSchema>) => {
    try {
      const response = await addUserRole(values);
      if (response) {
        toast.success("Rol başarıyla kullanıcıya atandı.");
        console.log("response:", response);
        form.reset();
      } else {
        console.log("fdsgfd");
      }
    } catch (error) {
      toast.error("Server error, try again later.");
    }
  };

  return (
    <div className="w-96">
      <Card className="shadow-2xl border-none text-white">
        <CardHeader>
          <CardTitle className="text-center">Add User Role</CardTitle>
        </CardHeader>
        <CardContent>
          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
              <FormField
                control={form.control}
                name="roleId"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Role</FormLabel>
                    <FormControl>
                      <select
                        {...field}
                        className="w-full p-2 border rounded-lg "
                      >
                        {roles.map((role) => (
                          <option
                            className="bg-[#0C1821] shadow-2xl"
                            key={role.id}
                            value={role.id}
                          >
                            {role.name}
                          </option>
                        ))}
                      </select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="userId"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>UserName</FormLabel>
                    <FormControl>
                      <select
                        {...field}
                        className="w-full p-2 border rounded-lg"
                      >
                        {users.map((user) => (
                          <option
                            className="bg-[#0C1821] shadow-2xl"
                            key={user.id}
                            value={user.id}
                          >
                            {user.userName}
                          </option>
                        ))}
                      </select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <Button
                type="submit"
                className="w-full bg-blue-700 hover:bg-blue-900 cursor-pointer"
              >
                Add
              </Button>
            </form>
          </Form>
        </CardContent>
      </Card>
    </div>
  );
}

export default AddUserRole;
