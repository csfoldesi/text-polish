import * as z from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { Form, FormControl, FormDescription, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Button } from "@/components/ui/button";
import { useTest } from "../-api/use-test";
import { useState } from "react";
import { Textarea } from "@/components/ui/textarea";

const formSchema = z.object({
  text: z.string().min(1, { message: "Text is required" }),
});

export const TextForm = () => {
  const { runTest } = useTest();
  const [result, setResult] = useState<string | null>(null);

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      text: "",
    },
  });

  async function onSubmit(values: z.infer<typeof formSchema>) {
    setResult(null);
    const data = await runTest({ ...values });
    setResult(data);
  }

  return (
    <>
      <div className="flex flex-col gap-4 max-w-[800px] rounded-lg border bg-card text-card-foreground shadow-sm p-4 m-4">
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
            <FormField
              control={form.control}
              name="text"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Text to polish</FormLabel>
                  <FormControl>
                    <Textarea placeholder="your text" {...field} />
                  </FormControl>
                  <FormDescription>Enter the text you want to polish</FormDescription>
                  <FormMessage />
                </FormItem>
              )}
            />
            <Button type="submit">Submit</Button>
          </form>
        </Form>
      </div>
      {result && (
        <div className="flex flex-col gap-4 max-w-[800px] rounded-lg border bg-card text-card-foreground shadow-sm p-4 m-4">
          <h2>{result}</h2>
        </div>
      )}
    </>
  );
};
