import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import z from "zod";
import { addRole } from "../services/AuthService";
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
import { Input } from "./ui/input";
import { Button } from "./ui/button";

const formSchema = z.object({
  roleName: z.string().min(2, {
    message: "Username must be at least 2 characters.",
  }),
});
function AddRole() {
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      roleName: "",
    },
  });

  async function onSubmit(values: z.infer<typeof formSchema>) {
    try {
      const data = {
        name: values.roleName,
      };
      const response = await addRole(data);

      if (response) {
        toast.success("Added role successful!");
        console.log("response:", response);
        form.reset();
      } else {
        toast.error(response.message || "Added role failed");
      }
    } catch (error) {
      console.error(error);
      toast.error("Server error, try again later.");
    }
  }
  return (
    <div className="w-96">
      <Card className="shadow-2xl border-none text-white">
        <CardHeader>
          <CardTitle className="text-center">Add Role</CardTitle>
        </CardHeader>
        <CardContent>
          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
              <FormField
                control={form.control}
                name="roleName"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Role Name</FormLabel>
                    <FormControl>
                      <Input {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <Button
                type="submit"
                className="w-full bg-blue-700 hover:bg-blue-900 cursor-pointer"
              >
                Add Role
              </Button>
            </form>
          </Form>
        </CardContent>
      </Card>
    </div>
  );
}

export default AddRole;
